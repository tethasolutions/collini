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
            CreateMap<Activity, ActivityDto>()
                .MapMember(x => x.JobCode, y => y.Job.Number + "/" + y.Job.Year)
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.Name + " " + y.Job.Customer.Surname);

            CreateMap<ActivityDto, Activity>()
                .Ignore(x=>x.StatusChangedOn)
                .Ignore(x=>x.Operator)
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Notes)
                .IgnoreCommonMembers();

            CreateMap<Activity, ActivityViewModel>()
                .MapMember(x => x.JobId, y => y.Job.Id)
                .MapMember(x => x.JobCode, y => y.Job.Number + "/" + y.Job.Year)
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.Name + " " + y.Job.Customer.Surname);
                

            CreateMap<User, CalendarResourceViewModel>()
                .MapMember(x=>x.Description,y=>y.Name + " " + y.Surname)
                .MapMember(x=>x.Color,y=>y.ColorHex ?? "");

        }
    }
}
