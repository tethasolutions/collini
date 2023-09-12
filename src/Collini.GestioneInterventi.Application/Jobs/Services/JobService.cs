
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Jobs.Services
{
    public interface IJobService
    {
        Task<IEnumerable<JobReadModel>> GetAllJobs();
        Task<JobDetailDto> UpdateJob(long id, JobDetailDto jobDto);
        Task<JobDetailDto> CreateJob(JobDetailDto jobDto);
        Task DeleteJob(long id);
        Task<JobDetailReadModel> GetJobDetail(long id);
        Task<IEnumerable<ProductTypeDto>> GetJobProductTypes();
        Task<IEnumerable<JobSourceDto>> GetJobSources();
        Task<IEnumerable<ContactReadModel>> GetJobCustomers();
        Task<IEnumerable<ContactReadModel>> GetJobSuppliers();        
        Task<IEnumerable<JobOperatorDto>> GetOperators();
        Task<JobCountersDto> GetJobCounters();
        Task<IEnumerable<JobDetailReadModel>> GetJobsAcceptance();
        Task<IEnumerable<JobDetailReadModel>> GetJobsActive();
        Task<IEnumerable<JobDetailReadModel>> GetJobsCompleted();
        Task<IEnumerable<JobDetailReadModel>> GetJobsBilling();
        Task<IEnumerable<JobDetailReadModel>> GetJobsPaid();
        Task<IEnumerable<JobSearchReadModel>> GetJobsSearch();
        Task<Job>  GetJob(long id);
    }

    public class JobService : IJobService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Job> jobRepository;
        private readonly IRepository<ProductType> productTypeRepository;
        private readonly IRepository<JobSource> jobSourceRepository;
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<Quotation> quotationRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Activity> activityRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Note> noteRepository;

        private readonly IColliniDbContext dbContext;

        public JobService(
            IMapper mapper,
            IRepository<Job> jobRepository,
            IRepository<ProductType> productTypeRepository,
            IColliniDbContext dbContext, IRepository<JobSource> jobSourceRepository, IRepository<User> userRepository,
            IRepository<Quotation> quotationRepository, IRepository<Order> orderRepository,
            IRepository<Activity> activityRepository, IRepository<Contact> contactRepository, IRepository<Note> noteRepository)
        {
            this.productTypeRepository = productTypeRepository;
            this.mapper = mapper;
            this.jobRepository = jobRepository;
            this.dbContext = dbContext;
            this.jobSourceRepository = jobSourceRepository;
            this.userRepository = userRepository;
            this.quotationRepository = quotationRepository;
            this.orderRepository = orderRepository;
            this.activityRepository = activityRepository;
            this.contactRepository = contactRepository;
            this.noteRepository = noteRepository;
        }

        public async Task<IEnumerable<JobReadModel>> GetAllJobs()
        {
            var jobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ToArrayAsync();

            return jobs.MapTo<IEnumerable<JobReadModel>>(mapper);
        }

        public async Task<JobDetailDto> UpdateJob(long id, JobDetailDto jobDto)
        {

            if (id == 0)
                throw new ColliniException("Impossibile aggiornare un job con id 0");

            var job= await jobRepository
                .Query()
                .AsNoTracking()
                //.Include(x=>x.Customer)
                //.Include(x=>x.CustomerAddress)
                //.Include(x=>x.Notes)
                //.Include(x=>x.Orders)
                //.Include(x=>x.Quotations)
                //.Include(x=>x.Source)
                //.Include(x=>x.ProductType)
                //.Include(x=>x.Activities)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ColliniException($"Impossibile trovare job con id {id}");

            //se sto cambiando stato e il nuovo stato è "in fatturazione" allora sposto la data di scadenza avanti di 30 gg
            if (job.Status != jobDto.Status && jobDto.Status == JobStatus.Billing)
            {
                jobDto.ExpirationDate = DateTime.Now.AddDays(30);
            }

            jobDto.MapTo(job, mapper);
            jobRepository.Update(job);
            await dbContext.SaveChanges();

            return job.MapTo<JobDetailDto>(mapper);

        }

        public async Task<JobDetailDto> CreateJob(JobDetailDto jobDto)
        {
            if (jobDto.Id > 0)
                throw new ColliniException("Impossibile creare un nuovo job con un id già esistente");

            var job = jobDto.MapTo<Job>(mapper);
            
            // TODO MB Introdurre un campo "Data commessa" in Job, non usare il campo CreatedOn
            var year = job.JobDate.Year;
            var currentNumber = await jobRepository.Query()
                .Where(e => e.Year == year)
                .MaxAsync(e => (int?) e.Number);

            job.Year = year;
            job.Number = (currentNumber ?? 0) + 1;
            
            Note note = new Note();
            note.JobId = job.Id;
            note.Value = "Nota Richiesta";
            job.Notes.Add(note);
            await jobRepository.Insert(job);
            

           

            try
            {
                await dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

            return job.MapTo<JobDetailDto>(mapper);
        }

        public async Task<Job> GetJob(long id)
        {
            if (id == 0)
                throw new ColliniException("Impossibile recuperare un job con id 0");

            var job = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x=>x.Source)
                .Include(x=>x.ProductType)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ColliniException($"Impossibile trovare il job con id {id}");

            return job;
        }

       

        public async Task<JobDetailReadModel> GetJobDetail(long id)
        {
            if (id == 0)
                throw new ColliniException("Impossibile recuperare un job con id 0");

            var job = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x=>x.Source)
                .Include(x=>x.ProductType)
                .Include(x=>x.Notes)
                .ThenInclude(x=>x.Attachments)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ColliniException($"Impossibile trovare il job con id {id}");

            return job.MapTo<JobDetailReadModel>(mapper);
        }

        public async Task<IEnumerable<ProductTypeDto>> GetJobProductTypes()
        {
            var productTypes = await productTypeRepository
                .Query()
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToArrayAsync();

            return productTypes.MapTo<IEnumerable<ProductTypeDto>>(mapper);
        }

        public async Task<IEnumerable<JobSourceDto>> GetJobSources()
        {
            var productTypes = await jobSourceRepository
                .Query()
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToArrayAsync();

            return productTypes.MapTo<IEnumerable<JobSourceDto>>(mapper);
        }

        public async Task<IEnumerable<ContactReadModel>> GetJobCustomers()
        {

            var customers = await contactRepository
                .Query()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Addresses)
                .Where(x => x.Type == ContactType.Customer)
                .OrderBy(x => x.CompanyName ?? x.Surname)
                .ToArrayAsync();

            return customers.MapTo<IEnumerable<ContactReadModel>>(mapper);
        }

        public async Task<IEnumerable<ContactReadModel>> GetJobSuppliers()
        {

            var customers = await contactRepository
                .Query()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Addresses)
                .Where(x => x.Type == ContactType.Supplier)
                .OrderBy(x => x.CompanyName)
                .ToArrayAsync();

            return customers.MapTo<IEnumerable<ContactReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobOperatorDto>> GetOperators()
        {
            var customers = await userRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.UserName != "Administrator")
                .OrderBy(x => x.Surname)
                .ToArrayAsync();

            return customers.MapTo<IEnumerable<JobOperatorDto>>(mapper);
        }

        public async Task<JobCountersDto> GetJobCounters()
        {
            var idJobActivities = await activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == ActivityStatus.Planned)
                .Select(x => x.JobId)
                .Distinct()
                .ToArrayAsync();

            var idJobOrders = await orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == OrderStatus.Pending || x.Status == OrderStatus.Sent)
                .Select(x => x.JobId)
                .Distinct()
                .ToArrayAsync();

            var idJobQuotation = await quotationRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == QuotationStatus.Pending || x.Status == QuotationStatus.Sent)
                .Select(x => x.JobId)
                .Distinct()
                .ToArrayAsync();


            var jobs = jobRepository
                .Query()
                .AsNoTracking();

          
            var Acceptance = jobs.Where(x => x.Status == JobStatus.Pending && x.Number != 0);
            var AcceptanceActive = Acceptance.Count(x => x.ExpirationDate >= DateTimeOffset.Now);
            var AcceptanceExpired = Acceptance.Count(x => x.ExpirationDate < DateTimeOffset.Now);

            var Actives = jobs.Where(x => x.Status == JobStatus.Working && (!idJobQuotation.Contains(x.Id) && !idJobActivities.Contains(x.Id) && !idJobOrders.Contains(x.Id)) && x.Number != 0);
            var ActivesActive = Actives.Count(x => x.ExpirationDate >= DateTimeOffset.Now);
            var ActivesExpired = Actives.Count(x => x.ExpirationDate < DateTimeOffset.Now);

            var Preventives = quotationRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == QuotationStatus.Pending ).ToList();
            var PreventivesActive = Preventives.Count(x => x.ExpirationDate >= DateTimeOffset.Now);
            var PreventivesExpired = Preventives.Count(x => x.ExpirationDate < DateTimeOffset.Now);

            var SupplierOrders = orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == OrderStatus.Pending).ToList();
            var SupplierOrdersActive = SupplierOrders.Count(x => x.ExpirationDate >= DateTimeOffset.Now);
            var SupplierOrdersExpired = SupplierOrders.Count(x => x.ExpirationDate < DateTimeOffset.Now);

            var Interventions = activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == ActivityStatus.Planned).ToList();
            var InterventionsActive = Interventions.Count(x => x.End >= DateTimeOffset.Now);
            var InterventionsExpired = Interventions.Count(x => x.End < DateTimeOffset.Now);

            var Completed = jobs.Where(x =>
                (x.Status == JobStatus.Completed || x.Status == JobStatus.Canceled) && x.Number != 0);
            var CompletedActive = Completed.Count(x => x.ExpirationDate >= DateTimeOffset.Now);
            var CompletedExpired = Completed.Count(x => x.ExpirationDate < DateTimeOffset.Now);

            var Billing = jobs.Where(x => x.Status == JobStatus.Billing && x.Number != 0);
            var BillingActive = Billing.Count(x => x.ExpirationDate >= DateTimeOffset.Now);
            var BillingExpired = Billing.Count(x => x.ExpirationDate < DateTimeOffset.Now);

            var ret = new JobCountersDto
            {
                Acceptance = new JobCounterDto()
                {
                    Active = AcceptanceActive,
                    Expired = AcceptanceExpired
                },
                Actives = new JobCounterDto()
                {
                    Active = ActivesActive,
                    Expired = ActivesExpired
                },
                Preventives = new JobCounterDto()
                {
                    Active = PreventivesActive,
                    Expired = PreventivesExpired
                },
                SupplierOrders = new JobCounterDto()
                {
                    Active = SupplierOrdersActive,
                    Expired = SupplierOrdersExpired
                },
                Interventions = new JobCounterDto()
                {
                    Active = InterventionsActive,
                    Expired = InterventionsExpired
                },
                Completed = new JobCounterDto()
                {
                    Active = CompletedActive,
                    Expired = CompletedExpired
                },
                Billing = new JobCounterDto()
                {
                    Active = BillingActive,
                    Expired = BillingExpired
                }
            };

            return ret;
        }

       

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsPaid()
        {
            var paidJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.Customer)
                .ThenInclude(x => x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x => x.ProductType)
                .Where(x => (x.Status == JobStatus.Billed || x.Status == JobStatus.Paid || x.Status == JobStatus.Warranty) && x.Number != 0)
                .ToArrayAsync();
            return paidJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsActive()
        {
            var idJobActivities = await activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == ActivityStatus.Planned)
                .Select(x => x.JobId)
                .Distinct()
                .ToArrayAsync();

            var idJobOrders = await orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == OrderStatus.Pending || x.Status == OrderStatus.Sent)
                .Select(x => x.JobId)
                .Distinct()
                .ToArrayAsync();

            var idJobQuotation = await quotationRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == QuotationStatus.Pending || x.Status == QuotationStatus.Sent)
                .Select(x => x.JobId)
                .Distinct()
                .ToArrayAsync();

            var activesJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x=>x.ProductType)
                .Where(x => x.Status == JobStatus.Working && (!idJobQuotation.Contains(x.Id) && !idJobActivities.Contains(x.Id) && !idJobOrders.Contains(x.Id)) && x.Number != 0)
                .ToArrayAsync();
            return activesJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsCompleted()
        {
            var completedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.Customer)
                .ThenInclude(x => x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x => x.ProductType)
                .Where(x => (x.Status == JobStatus.Completed || x.Status == JobStatus.Canceled) && x.Number != 0)
                .ToArrayAsync();
            return completedJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsBilling()
        {
            var billingJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x=>x.ProductType)
                .Where(x => x.Status == JobStatus.Billing && x.Number != 0)
                .ToArrayAsync();
            return  billingJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsAcceptance()
        {
            var acceptancedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.CustomerAddress)
                .Include(x=>x.ProductType)
                .Where(x => x.Status == JobStatus.Pending && x.Number != 0)
                .ToArrayAsync();
            return acceptancedJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobSearchReadModel>> GetJobsSearch()
        {
            var searchedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => (x.Number != 0))
                .OrderByDescending(x => x.JobDate)
                .ThenBy(x => x.Customer.CompanyName)
                .ThenBy(x => x.Customer.Surname)
                .Project<JobSearchReadModel>(mapper)
                .ToArrayAsync();
            return searchedJobs;
        }

        public async Task DeleteJob(long id)
        {
            if (id == 0)
                throw new ColliniException("Impossible eliminare la richiesta con id 0");

            var job = await jobRepository
                .Query()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ColliniException($"Impossibile trovare la richiesta con id {id}");

            jobRepository.Delete(job);
            await dbContext.SaveChanges();
        }

    }
}
