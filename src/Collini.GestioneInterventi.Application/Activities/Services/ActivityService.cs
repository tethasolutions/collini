using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Activities.Services
{
    public interface IActivityService
    {
        Task<ActivityDto> CreateActivity(ActivityDto activityDto);

        Task<ActivityDto> UpdateActivity(long id, ActivityDto activityDto);

        Task<ActivityViewModel> GetActivity(long id);

        Task<CalendarViewModel> GetCalendar();

    }

    public class ActivityService:IActivityService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Activity> activityRepository;
        private readonly IJobService jobService;
        private readonly IColliniDbContext dbContext;

        public ActivityService(
            IMapper mapper,
            IRepository<Activity> activityRepository,
            IColliniDbContext dbContext, IJobService jobService)
        {
            this.mapper = mapper;
            this.activityRepository = activityRepository;
            this.dbContext = dbContext;
            this.jobService = jobService;
        }

        public async Task<ActivityDto> CreateActivity(ActivityDto activityDto)
        {
            var activity = activityDto.MapTo<Activity>(mapper);

            await activityRepository.Insert(activity);

            var job = await jobService.GetJobDtoForUpdate(activityDto.JobId);
            if (job == null)
                throw new ApplicationException("Job non trovato");
            if (job.Status == JobStatus.Pending)
                job.Status = JobStatus.Working;
            await jobService.UpdateJob(job.Id.Value, job.MapTo<JobDetailDto>(mapper));

            await dbContext.SaveChanges();
 
            activity.Job = await jobService.GetJob(activityDto.JobId);
            
            return activity.MapTo<ActivityDto>(mapper);
        }

        public async Task<ActivityDto> UpdateActivity(long id, ActivityDto activityDto)        
        {
            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare una attività con id 0");

            var activity= await activityRepository
                .Query()
                //.Include(x=>x.Operator)
                //.Include(x=>x.Job)
                //.Include(x=>x.Notes)
                .AsNoTracking()
                .Include(x=>x.Job)
                .ThenInclude(x=>x.Customer)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ApplicationException($"Impossibile trovare attività con id {id}");

            activityDto.MapTo(activity, mapper);
            activityRepository.Update(activity);
            await dbContext.SaveChanges();
            return activity.MapTo<ActivityDto>(mapper);
        }

        public async Task<ActivityViewModel> GetActivity(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un activity con id 0");

            var activity = await activityRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Job)
                .ThenInclude(y=>y.Customer)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ApplicationException($"Impossibile trovare l'activity con id {id}");

            return activity.MapTo<ActivityViewModel>(mapper);
           
        }

        public async Task<CalendarViewModel> GetCalendar()
        {
            CalendarViewModel calendar = new CalendarViewModel();

            var activities = await activityRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Job)
                
                .ThenInclude(y=>y.Customer)
                .Include(x=>x.Operator)
                .ToArrayAsync();

            calendar.Activities = activities.MapTo<IEnumerable<ActivityViewModel>>(mapper).ToList();
            var operators = activities.Select(x => x.Operator).DistinctBy(x=>x.Id);
                var resources =  operators.MapTo<IEnumerable<CalendarResourceViewModel>>(mapper).ToList();
            calendar.Resources = resources;
            return calendar;

        }
    }
}
