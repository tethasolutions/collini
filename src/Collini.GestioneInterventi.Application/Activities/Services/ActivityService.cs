﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Collini.GestioneInterventi.Framework.Session;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Activities.Services
{
    public interface IActivityService
    {
        IQueryable<ActivityDto> GetActivities();

        Task<ActivityDto> CreateActivity(ActivityDto activityDto);

        Task UpdateActivity(long id, ActivityDto activityDto);
        Task CopyActivity(CopyActivityDto copyActivityDto);

        Task<ActivityViewModel> GetActivity(long id);

        Task<CalendarViewModel> GetCalendar();

    }

    public class ActivityService:IActivityService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Activity> activityRepository;
        private readonly IJobService jobService;
        private readonly IColliniDbContext dbContext;
        private readonly IColliniSession session;

        public ActivityService(
            IMapper mapper,
            IRepository<Activity> activityRepository,
            IColliniDbContext dbContext, IJobService jobService, IColliniSession session)
        {
            this.mapper = mapper;
            this.activityRepository = activityRepository;
            this.dbContext = dbContext;
            this.jobService = jobService;
            this.session = session;
        }

        public async Task<ActivityDto> CreateActivity(ActivityDto activityDto)
        {
            var activity = activityDto.MapTo<Activity>(mapper);

            await activityRepository.Insert(activity);

            var job = await jobService.GetJob(activityDto.JobId);
            if (job == null)
                throw new ApplicationException("Job non trovato");
            //if (job.Status == JobStatus.Pending)
            job.Status = JobStatus.Working;

            if (activity.Status != ActivityStatus.Planned)
                job.Status = JobStatus.Completed;
            await jobService.UpdateJob(job.Id, job.MapTo<JobDetailDto>(mapper));

            await dbContext.SaveChanges();
 
            activity.Job = await jobService.GetJob(activityDto.JobId);
            
            return activity.MapTo<ActivityDto>(mapper);
        }

        public async Task UpdateActivity(long id, ActivityDto activityDto)        
        {
            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare una attività con id 0");

            var activity= await activityRepository
                .Query()
                .Include(x=>x.Job)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ApplicationException($"Impossibile trovare attività con id {id}");

            activityDto.MapTo(activity, mapper);

            if (activity.Status is ActivityStatus.CompletedSuccessfully 
                                or ActivityStatus.CompletedUnsuccessfully)
            {
                activity.Job.Status = JobStatus.Completed;
                activity.Job.ResultNote += (activity.Job.ResultNote is {Length: > 0} ? Environment.NewLine + Environment.NewLine : "") + activity.Description;
            }

            if (activity.Status is ActivityStatus.Canceled)
            {
                activity.Job.Status = JobStatus.Working;
                activity.Job.ResultNote += (activity.Job.ResultNote is {Length: > 0} ? Environment.NewLine + Environment.NewLine : "") + activity.Description;
            }

            activityRepository.Update(activity);
            
            await dbContext.SaveChanges();
        }

        public async Task CopyActivity(CopyActivityDto copyActivityDto)
        {
            var activity = await activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == copyActivityDto.Id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ApplicationException($"Impossibile trovare attività con id {copyActivityDto.Id}");

            activity.Id = 0;
            activity.OperatorId = copyActivityDto.NewOperatorId;
            await activityRepository.Insert(activity);

            await dbContext.SaveChanges();
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
                .Include(x => x.Job)
                .ThenInclude(y=>y.CustomerAddress)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (activity == null)
                throw new ApplicationException($"Impossibile trovare l'activity con id {id}");

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
                .Where(x => x.Status == ActivityStatus.Planned)
                .Project<ActivityDto>(mapper);

            return activities;
        }
    }
}
