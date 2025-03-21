using System.Net.Mail;
using System.Net;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Collini.GestioneInterventi.Framework.Session;
using Microsoft.EntityFrameworkCore;
using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Collini.GestioneInterventi.Application.Activities.Services
{
    public interface IActivityService
    {
        IQueryable<ActivityDto> GetActivities();

        Task<ActivityDto> CreateActivity(ActivityDto activityDto);

        Task UpdateActivity(long id, ActivityDto activityDto);
        Task CopyActivity(CopyActivityDto copyActivityDto);
        Task SaveAndNewQuotation(long id, ActivityDto activityDto);

        Task<ActivityViewModel> GetActivity(long id);

        Task<CalendarViewModel> GetCalendar();
        Task PayJob(long id);
        Task DeleteActivity(long id);

    }

    public class ActivityService:IActivityService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Activity> activityRepository;
        private readonly IJobService jobService;
        private readonly IRepository<Quotation> quotationRepository;
        private readonly IColliniDbContext dbContext;
        private readonly IColliniSession session; 
        private readonly IRepository<SmtpSettings> smtpRepository;

        public ActivityService(
            IMapper mapper,
            IRepository<Activity> activityRepository,
            IColliniDbContext dbContext,
            IJobService jobService,
            IRepository<Quotation> quotationRepository,
            IRepository<SmtpSettings> smtpRepository,
            IColliniSession session,
            IConfiguration configuration)
        {
            this.mapper = mapper;
            this.activityRepository = activityRepository;
            this.dbContext = dbContext;
            this.jobService = jobService;
            this.quotationRepository = quotationRepository;
            this.session = session;
            this.smtpRepository = smtpRepository;
        }

        public async Task<ActivityDto> CreateActivity(ActivityDto activityDto)
        {
            var activity = activityDto.MapTo<Activity>(mapper);

            await activityRepository.Insert(activity);

            var job = await jobService.GetJob(activityDto.JobId);
            if (job == null)
                throw new ColliniException("Job non trovato");
            //if (job.Status == JobStatus.Pending)
            job.Status = JobStatus.Working;

            //activity.Description = job.ResultNote;

            if (activity.Status != ActivityStatus.Planned)
                job.Status = JobStatus.Completed;
            await jobService.UpdateJob(job.Id, job.MapTo<JobDetailDto>(mapper));

            await dbContext.SaveChanges();
 
            activity.Job = await jobService.GetJob(activityDto.JobId);

            await SendNotificationEmail(activity.Id, true);

            return activity.MapTo<ActivityDto>(mapper);
        }

        public async Task UpdateActivity(long id, ActivityDto activityDto)        
        {
            if (id == 0)
                throw new ColliniException("Impossibile aggiornare una attività con id 0");

            var activity= await activityRepository
                .Query()
                .Include(x=>x.Job)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ColliniException($"Impossibile trovare attività con id {id}");

            activityDto.MapTo(activity, mapper);

            if (activity.Job.Number != 0 && activity.Job.Status != JobStatus.Billed && activity.Job.Status != JobStatus.Paid && activity.Job.Status != JobStatus.Warranty)
            {
                if (activity.Status is ActivityStatus.CompletedSuccessfully
                                    or ActivityStatus.CompletedUnsuccessfully)
                {
                    activity.Job.Status = JobStatus.Completed;

                    //14/04/2024 aggiunta chiusura automatica anche delle altre attività se lo stato è completato OK
                    if (activity.Status == ActivityStatus.CompletedSuccessfully)
                    {
                        var activities = await activityRepository.Query()
                            .Where(x => x.JobId == activity.JobId && x.Id != activity.Id)
                            .ToArrayAsync();
                        foreach (Activity act in activities)
                        {
                            act.Status = activity.Status;
                            activityRepository.Update(act);
                        }
                    }
                }

                if (activity.Status is ActivityStatus.Canceled
                                    or ActivityStatus.ToComplete
                                    or ActivityStatus.CompletedQuotation)
                {
                    activity.Job.Status = JobStatus.Working;
                }
                
                //activity.Job.ResultNote += (activity.Job.ResultNote is { Length: > 0 } ? Environment.NewLine + Environment.NewLine : "") + activity.Description;
                activity.Job.ResultNote = activity.Description;

            }

            activityRepository.Update(activity);

            await dbContext.SaveChanges();
            
            if (activity.Status == ActivityStatus.Planned) await SendNotificationEmail(activity.Id, false);

        }

        public async Task CopyActivity(CopyActivityDto copyActivityDto)
        {
            var activity = await activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == copyActivityDto.Id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ColliniException($"Impossibile trovare attività con id {copyActivityDto.Id}");

            activity.Id = 0;
            activity.OperatorId = copyActivityDto.NewOperatorId;
            activity.Start = copyActivityDto.Start;
            activity.End = copyActivityDto.End;
            await activityRepository.Insert(activity);

            await dbContext.SaveChanges();
            await SendNotificationEmail(activity.Id, true);
        }

        public async Task<ActivityViewModel> GetActivity(long id)
        {
            if (id == 0)
                throw new ColliniException("Impossibile recuperare un activity con id 0");

            var activity = await activityRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Job)
                .ThenInclude(y=>y.Customer)
                .Include(x => x.Job)
                .ThenInclude(y=>y.CustomerAddress)
                .Include(x=>x.Job)
                .ThenInclude(y=>y.Notes)
                .ThenInclude(y=>y.Attachments)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ColliniException($"Impossibile trovare l'activity con id {id}");

            return activity.MapTo<ActivityViewModel>(mapper);
           
        }

        public async Task<CalendarViewModel> GetCalendar()
        {
            CalendarViewModel calendar = new CalendarViewModel();
            var user = session.CurrentUser;

            var activitiesQuery = activityRepository
                .Query()
                .AsNoTracking();

            if (session.CurrentUser.Role == Domain.Security.Role.Operator)
            {
                activitiesQuery = activitiesQuery
                    .Where(x => x.OperatorId == session.CurrentUser.UserId);
            }

            var activities = await activitiesQuery
                .Include(x => x.Job)
                .ThenInclude(y => y.Customer)
                .Include(x => x.Job)
                .ThenInclude(y => y.CustomerAddress)
                .Include(x => x.Operator)
                .ToArrayAsync();

            try
            {
                calendar.Activities = activities.MapTo<IEnumerable<ActivityViewModel>>(mapper).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            try
            {
                var operators = activities.Select(x => x.Operator).DistinctBy(x=>x.Id);
                var resources =  operators.MapTo<IEnumerable<CalendarResourceViewModel>>(mapper).ToList();
                calendar.Resources = resources;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

            
            return calendar;

        }

        public IQueryable<ActivityDto> GetActivities()
        {
            var activities = activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == ActivityStatus.Planned || x.Status == ActivityStatus.MaterialReady)
                .Project<ActivityDto>(mapper);

            return activities;
        }

        public async Task PayJob(long id)
        {
            if (id == 0)
                throw new ColliniException("Impossible salvare intervento con id 0");

            var activity = await activityRepository
                .Query()
                .Include(x => x.Job)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ColliniException($"Impossibile trovare l'intervento con id {id}");

            activity.Status = ActivityStatus.CompletedSuccessfully;
            activity.Job.Status = JobStatus.Completed;
            //activity.Job.ResultNote += (activity.Job.ResultNote is { Length: > 0 } ? Environment.NewLine + Environment.NewLine : "") + activity.Description; 
            activity.Job.ResultNote = activity.Description;
            activity.Job.IsPaid= true;

            activityRepository.Update(activity);
            
            await activityRepository.Update(x => x.JobId == activity.JobId && x.Id != activity.Id, e => e.Status = ActivityStatus.CompletedSuccessfully);

            await dbContext.SaveChanges();
        }

        public async Task SaveAndNewQuotation(long id, ActivityDto activityDto)
        {
            if (id == 0)
                throw new ColliniException("Impossibile aggiornare una attività con id 0");

            var activity = await activityRepository
                .Query()
                .Include(x => x.Job)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ColliniException($"Impossibile trovare attività con id {id}");

            activityDto.Status = ActivityStatus.CompletedQuotation;

            activityDto.MapTo(activity, mapper);

            if (activity.Job.Number != 0 && activity.Job.Status != JobStatus.Billed && activity.Job.Status != JobStatus.Paid && activity.Job.Status != JobStatus.Warranty)
            {
                if (activity.Status is ActivityStatus.CompletedSuccessfully
                                    or ActivityStatus.CompletedUnsuccessfully)
                {
                    activity.Job.Status = JobStatus.Completed;
                }

                if (activity.Status is ActivityStatus.Canceled
                                    or ActivityStatus.ToComplete
                                    or ActivityStatus.CompletedQuotation)
                {
                    activity.Job.Status = JobStatus.Working;
                }

                activity.Job.ResultNote = activity.Description;

                Quotation quotation = new Quotation();
                quotation.Id = 0;
                quotation.Status = QuotationStatus.Pending;
                quotation.JobId = activity.JobId;
                quotation.ExpirationDate = DateTimeOffset.Now.Date.AddDays(2);
                quotation.Amount = 0;

                await quotationRepository.Insert(quotation);
            }

            activityRepository.Update(activity);

            await dbContext.SaveChanges();
        }

        public async Task DeleteActivity(long id)
        {
            if (id == 0)
                throw new ColliniException("Impossible eliminare l'intervento con id 0");

            var activity = await activityRepository
                .Query()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ColliniException($"Impossibile trovare l'intervento con id {id}");

            activityRepository.Delete(activity);
            await dbContext.SaveChanges();
        }

        private async Task SendNotificationEmail(long activityId, bool isNew)
        {
            var activity = await activityRepository.Query()
                .AsNoTracking()
                .Include(x => x.Operator)
                .Include(x => x.Job)
                .ThenInclude(x => x.Customer)
                .Where(u => u.Id == activityId)
                .FirstOrDefaultAsync();

            if (activity == null)
            {
                throw new ColliniException("L'attività non è stata trovata.");
            }

            if (string.IsNullOrEmpty(activity.Operator?.EmailAddress))
            {
                throw new ColliniException("L'operatore non ha un'email associata.");
            }

            var smtpSettings = await smtpRepository.Query().FirstOrDefaultAsync();
            if (smtpSettings == null)
            {
                throw new ColliniException("Le impostazioni SMTP non sono state configurate.");
            }

            SmtpClient SmtpClient = new SmtpClient("smtp.sendgrid.net", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("apikey", "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"),
                EnableSsl = false
            };
            var bodyPrefix = string.Empty;
            if (isNew) { bodyPrefix = $"<p>Ciao {activity.Operator?.Name},<br/>ti è stato assegnato un nuovo intervento</p>"; }
            else { bodyPrefix = $"<p>Ciao {activity.Operator?.Name},<br/>un intervento è stato modificato</p>"; }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings.From),
                Subject = "Nuovo Intervento Assegnato",

                Body = $"{bodyPrefix}<p><strong>Cliente:</strong> {activity.Job?.Customer?.CompanyName} {activity.Job?.Customer?.Surname} {activity.Job?.Customer?.Name}</p>" +
                $"<p><strong>Descrizione:</strong> {activity.Job?.Description}<br/>{activity.Description}</p>" +
                $"<p><strong>Inizio:</strong> {activity.Start.ToLocalTime().ToString("dd/MM/yyyy HH:mm")}<br/>" +
                $"<strong>Fine:</strong> {activity.End.ToLocalTime().ToString("dd/MM/yyyy HH:mm")}</p>" +
                $"<p>Cordiali saluti,\nStaff Collini</p>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(activity.Operator?.EmailAddress);


            try
            {
                SmtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                throw new ColliniException("Errore durante l'invio della mail", e);
            }
        }
    }
}
