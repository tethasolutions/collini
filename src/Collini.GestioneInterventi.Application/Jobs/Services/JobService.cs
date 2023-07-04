
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
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Jobs.Services
{
    public interface IJobService
    {
        Task<IEnumerable<JobReadModel>> GetAllJobs();
        Task<JobDetailDto> UpdateJob(long id, JobDetailDto jobDto);
        Task<JobDetailDto> CreateJob(JobDetailDto jobDto);
        Task<JobDetailReadModel> GetJobDetail(long id);
        Task<IEnumerable<ProductTypeDto>> GetJobProductTypes();
        Task<IEnumerable<JobSourceDto>> GetJobSources();
        Task<IEnumerable<ContactReadModel>> GetJobCustomers();
        Task<IEnumerable<JobOperatorDto>> GetOperators();
        Task<JobCountersDto> GetJobCounters();
        Task<IEnumerable<JobReadModel>> GetJobsBilled();
        Task<IEnumerable<JobReadModel>> GetJobsAcceptance();
        Task<IEnumerable<JobReadModel>> GetJobsActive();
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
        private readonly IColliniDbContext dbContext;

        public JobService(
            IMapper mapper,
            IRepository<Job> jobRepository,
            IRepository<ProductType> productTypeRepository,
            IColliniDbContext dbContext, IRepository<JobSource> jobSourceRepository, IRepository<User> userRepository,
            IRepository<Quotation> quotationRepository, IRepository<Order> orderRepository,
            IRepository<Activity> activityRepository)
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
        }

        public async Task<IEnumerable<JobReadModel>> GetAllJobs()
        {
            var jobs = await jobRepository
                .Query()
                .AsNoTracking()
                .ToArrayAsync();

            return jobs.MapTo<IEnumerable<JobReadModel>>(mapper);
        }

        public async Task<JobDetailDto> UpdateJob(long id, JobDetailDto jobDto)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare un job con id 0");

            var job = await jobRepository
                .Query()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ApplicationException($"Impossibile trovare il job con id {id}");

            jobDto.MapTo(job, mapper);
            jobRepository.Update(job);
            await dbContext.SaveChanges();
            return job.MapTo<JobDetailDto>(mapper);
        }

        public async Task<JobDetailDto> CreateJob(JobDetailDto jobDto)
        {
            if (jobDto.Id > 0)
                throw new ApplicationException("Impossibile creare un nuovo job con un id già esistente");

            var job = jobDto.MapTo<Job>(mapper);
            jobRepository.Insert(job);
            await dbContext.SaveChanges();

            return job.MapTo<JobDetailDto>(mapper);
        }

        public async Task<JobDetailReadModel> GetJobDetail(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un job con id 0");

            var job = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ApplicationException($"Impossibile trovare il job con id {id}");

            return job.MapTo<JobDetailReadModel>(mapper);
        }

        public async Task<IEnumerable<ProductTypeDto>> GetJobProductTypes()
        {
            var productTypes = await productTypeRepository
                .Query()
                .AsNoTracking()
                .ToArrayAsync();

            return productTypes.MapTo<IEnumerable<ProductTypeDto>>(mapper);
        }

        public async Task<IEnumerable<JobSourceDto>> GetJobSources()
        {
            var productTypes = await jobSourceRepository
                .Query()
                .AsNoTracking()
                .ToArrayAsync();

            return productTypes.MapTo<IEnumerable<JobSourceDto>>(mapper);
        }

        public async Task<IEnumerable<ContactReadModel>> GetJobCustomers()
        {

            var customers = await contactRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.Addresses)
                .Where(x => x.Type == ContactType.Customer)
                .OrderBy(x => x.CompanyName ?? x.Surname)
                .ToArrayAsync();

            return customers.MapTo<IEnumerable<ContactReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobOperatorDto>> GetOperators()
        {
            var customers = await userRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Role == Role.Operator)
                .OrderBy(x => x.Surname)
                .ToArrayAsync();

            return customers.MapTo<IEnumerable<JobOperatorDto>>(mapper);
        }

        public async Task<JobCountersDto> GetJobCounters()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == JobStatus.Billed)
                .ToArrayAsync();

            var acceptedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == JobStatus.Pending)
                .ToArrayAsync();

            var activeJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == JobStatus.Working)
                .ToArrayAsync();

            var preventives = await quotationRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == QuotationStatus.Pending)
                .ToArrayAsync();

            var supplierorders = await orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == OrderStatus.Pending)
                .ToArrayAsync();

            var interventions = await activityRepository
                .Query()
                .Where(x => x.Status == ActivityStatus.Planned)
                .AsNoTracking()
                .ToArrayAsync();


            return new JobCountersDto
            {
                Acceptance = new JobCounterDto()
                {
                    Active = acceptedJobs.Count(x => x.ExpirationDate <= DateTimeOffset.Now),
                    Expired = acceptedJobs.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Actives = new JobCounterDto()
                {
                    Active = activeJobs.Count(x => x.ExpirationDate <= DateTimeOffset.Now),
                    Expired = activeJobs.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Preventives = new JobCounterDto()
                {
                    Active = preventives.Count(x => x.ExpirationDate <= DateTimeOffset.Now),
                    Expired = preventives.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                SupplierOrders = new JobCounterDto()
                {
                    Active = supplierorders.Count(x => x.ExpirationDate <= DateTimeOffset.Now),
                    Expired = supplierorders.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Interventions = new JobCounterDto()
                {
                    Active = interventions.Count(x => x.Start <= DateTimeOffset.Now && x.End >= DateTimeOffset.Now),
                    Expired = interventions.Count(x => x.End < DateTimeOffset.Now)
                },
                Billed = new JobCounterDto()
                {
                    Active = billedJobs.Count(x => x.ExpirationDate <= DateTimeOffset.Now),
                    Expired = billedJobs.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                }
            };
        }

        public async Task<IEnumerable<JobReadModel>> GetJobsBilled()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == JobStatus.Billed)
                .ToArrayAsync();
            return  billedJobs.MapTo<IEnumerable<JobReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobReadModel>> GetJobsActive()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == JobStatus.Working)
                .ToArrayAsync();
            return  billedJobs.MapTo<IEnumerable<JobReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobReadModel>> GetJobsAcceptance()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == JobStatus.Pending)
                .ToArrayAsync();
            return  billedJobs.MapTo<IEnumerable<JobReadModel>>(mapper);
        }

    }
}
