using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Activities
{
   
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityDto, Activity>()
                .Ignore(x=>x.Description)
                .Ignore(x=>x.StatusChangedOn)
                .Ignore(x=>x.Operator)
                .Ignore(x=>x.JobId)
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Notes)
                .IgnoreCommonMembers();

            CreateMap<Activity, ActivityViewModel>()
                .MapMember(x => x.JobId, y => y.Job.Id);
                

            CreateMap<User, CalendarResourceViewModel>()
                .MapMember(x=>x.Description,y=>y.Surname + " " + y.Name)
                .MapMember(x=>x.Color,y=>y.ColorHex ?? "");

        }
    }
}
