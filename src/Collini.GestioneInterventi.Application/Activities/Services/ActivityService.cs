using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
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
        private readonly IColliniDbContext dbContext;

        public ActivityService(
            IMapper mapper,
            IRepository<Activity> activityRepository,
            IColliniDbContext dbContext)
        {
            this.mapper = mapper;
            this.activityRepository = activityRepository;
            this.dbContext = dbContext;
        }

        public async Task<ActivityDto> CreateActivity(ActivityDto activityDto)
        {
            var activity = activityDto.MapTo<Activity>(mapper);

            await activityRepository.Insert(activity);

            await dbContext.SaveChanges();

            return activity.MapTo<ActivityDto>(mapper);
        }

        public async Task<ActivityDto> UpdateActivity(long id, ActivityDto activityDto)        
        {
            var activity = await activityRepository.Get(id);

            if (activity == null)
            {
                throw new NotFoundException(typeof(Activity), id);
            }
            activityDto.MapTo(activity, mapper);

            await dbContext.SaveChanges();

            return activity.MapTo<ActivityDto>(mapper);
        }

        public async Task<ActivityViewModel> GetActivity(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un activity con id 0");

            var job = await activityRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ApplicationException($"Impossibile trovare l'activity con id {id}");

            return job.MapTo<ActivityViewModel>(mapper);
           
        }

        public async Task<CalendarViewModel> GetCalendar()
        {
            CalendarViewModel calendar = new CalendarViewModel();

            var activities = activityRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.Operator)
                .ToArrayAsync();

            calendar.Activities = activities.MapTo<IEnumerable<ActivityViewModel>>(mapper).ToList();
            calendar.Resources = (await activities).Select(x => x.Operator).Distinct()
                .MapTo<IEnumerable<CalendarResourceViewModel>>(mapper).ToList();
            return calendar;

        }
    }
}
