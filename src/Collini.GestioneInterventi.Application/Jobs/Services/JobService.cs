﻿
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
        Task<JobDetailReadModel> GetJobDetail(long id);
        Task<IEnumerable<ProductTypeDto>> GetJobProductTypes();
        Task<IEnumerable<JobSourceDto>> GetJobSources();
        Task<IEnumerable<ContactReadModel>> GetJobCustomers();
        Task<IEnumerable<ContactReadModel>> GetJobSuppliers();
        
        Task<IEnumerable<JobOperatorDto>> GetOperators();
        Task<JobCountersDto> GetJobCounters();
        Task<IEnumerable<JobDetailReadModel>> GetJobsBilled();
        Task<IEnumerable<JobDetailReadModel>> GetJobsAcceptance();
        Task<IEnumerable<JobDetailReadModel>> GetJobsCompleted();
        Task<IEnumerable<JobDetailReadModel>> GetJobsActive();
        Task<JobDetailDto> GetJobDtoForUpdate(long id);
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
        private readonly IColliniDbContext dbContext;

        public JobService(
            IMapper mapper,
            IRepository<Job> jobRepository,
            IRepository<ProductType> productTypeRepository,
            IColliniDbContext dbContext, IRepository<JobSource> jobSourceRepository, IRepository<User> userRepository,
            IRepository<Quotation> quotationRepository, IRepository<Order> orderRepository,
            IRepository<Activity> activityRepository, IRepository<Contact> contactRepository)
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
                throw new ApplicationException("Impossibile aggiornare un job con id 0");

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
                .SingleOrDefaultAsync();;

            if (job == null)
                throw new ApplicationException($"Impossibile trovare job con id {id}");

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
            
            // TODO MB Introdurre un campo "Data commessa" in Job, non usare il campo CreatedOn
            var year = DateTimeOffset.UtcNow.Year;
            var currentNumber = await jobRepository.Query()
                .Where(e => e.Year == year)
                .MaxAsync(e => (int?) e.Number);

            job.Year = year;
            job.Number = (currentNumber ?? 0) + 1;

            await jobRepository.Insert(job);
            await dbContext.SaveChanges();

            return job.MapTo<JobDetailDto>(mapper);
        }

        public async Task<Job> GetJob(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un job con id 0");

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
                throw new ApplicationException($"Impossibile trovare il job con id {id}");

            return job;
        }

        public async Task<JobDetailDto> GetJobDtoForUpdate(long id)
        {
            var job = await GetJob(id);

            return job.MapTo<JobDetailDto>(mapper);
        }

        public async Task<JobDetailReadModel> GetJobDetail(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un job con id 0");

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
            var jobs = jobRepository
                .Query()
                .AsNoTracking();

            var preventives = quotationRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == QuotationStatus.Pending);

            var supplierorders = orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Status == OrderStatus.Pending);

            var interventions = activityRepository
                .Query()
                .Where(x => x.Status == ActivityStatus.Planned)
                .AsNoTracking();


            var ret = new JobCountersDto
            {
                Acceptance = new JobCounterDto()
                {
                    Active = jobs.Where(x => x.Status == JobStatus.Pending).Count(x => x.ExpirationDate >= DateTimeOffset.Now),
                    Expired = jobs.Where(x => x.Status == JobStatus.Pending).Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Actives = new JobCounterDto()
                {
                    Active = jobs.Where(x => x.Status == JobStatus.Working).Count(x => x.ExpirationDate >= DateTimeOffset.Now),
                    Expired = jobs.Where(x => x.Status == JobStatus.Working).Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Preventives = new JobCounterDto()
                {
                    Active = preventives.Count(x => x.ExpirationDate >= DateTimeOffset.Now),
                    Expired = preventives.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                SupplierOrders = new JobCounterDto()
                {
                    Active = supplierorders.Count(x => x.ExpirationDate >= DateTimeOffset.Now),
                    Expired = supplierorders.Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Interventions = new JobCounterDto()
                {
                    Active = interventions.Count(x => x.End >= DateTimeOffset.Now),
                    Expired = interventions.Count(x => x.End < DateTimeOffset.Now)
                },
                Completed = new JobCounterDto()
                {
                    Active = jobs.Where(x => x.Status == JobStatus.Completed).Count(x => x.ExpirationDate >= DateTimeOffset.Now),
                    Expired = jobs.Where(x => x.Status == JobStatus.Completed).Count(x => x.ExpirationDate < DateTimeOffset.Now)
                },
                Billed = new JobCounterDto()
                {
                    Active = jobs.Where(x => x.Status == JobStatus.Billing).Count(x => x.ExpirationDate >= DateTimeOffset.Now),
                    Expired = jobs.Where(x => x.Status == JobStatus.Billing).Count(x => x.ExpirationDate < DateTimeOffset.Now)
                }
            };

            return ret;
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsBilled()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.ProductType)
                .Where(x => x.Status == JobStatus.Billed || x.Status == JobStatus.Billing)
                .ToArrayAsync();
            return  billedJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsActive()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.ProductType)
                .Where(x => x.Status == JobStatus.Working)
                .ToArrayAsync();
            return  billedJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsCompleted()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.Customer)
                .ThenInclude(x => x.Addresses)
                .Include(x => x.ProductType)
                .Where(x => x.Status == JobStatus.Completed)
                .ToArrayAsync();
            return billedJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

        public async Task<IEnumerable<JobDetailReadModel>> GetJobsAcceptance()
        {
            var billedJobs = await jobRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.Addresses)
                .Include(x=>x.ProductType)
                .Where(x => x.Status == JobStatus.Pending || x.Status == JobStatus.Canceled)
                .ToArrayAsync();
            return  billedJobs.MapTo<IEnumerable<JobDetailReadModel>>(mapper);
        }

    }
}
