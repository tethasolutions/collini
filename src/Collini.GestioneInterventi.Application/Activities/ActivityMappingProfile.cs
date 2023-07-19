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
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name + 
                    ((y.Job.Customer.Telephone != null) ? " - Tel: " + y.Job.Customer.Telephone : "") +
                    ((y.Job.CustomerAddress != null) ? " - " + y.Job.CustomerAddress.StreetAddress + " " + y.Job.CustomerAddress.City : ""))
                .MapMember(x => x.Operator, y => y.Operator.UserName);

            CreateMap<ActivityDto, Activity>()
                .Ignore(x=>x.StatusChangedOn)
                .Ignore(x=>x.Operator)
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Notes)
                .IgnoreCommonMembers();

            CreateMap<Activity, ActivityViewModel>()
                .MapMember(x => x.JobId, y => y.Job.Id)
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name +
                    ((y.Job.Customer.Telephone != null) ? " - Tel: " + y.Job.Customer.Telephone : "") +
                    ((y.Job.CustomerAddress != null) ? " - " + y.Job.CustomerAddress.StreetAddress + " " + y.Job.CustomerAddress.City : ""));

            CreateMap<User, CalendarResourceViewModel>()
                .MapMember(x=>x.Description,y=>y.Name + " " + y.Surname)
                .MapMember(x=>x.Color,y=>y.ColorHex ?? "");

        }
    }
}
