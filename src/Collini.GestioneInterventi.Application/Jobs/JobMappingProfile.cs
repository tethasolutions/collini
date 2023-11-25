using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Jobs
{

    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {

            CreateMap<Job, JobReadModel>();
            CreateMap<Contact, ContactReadModel>();

            CreateMap<JobDetailDto, Job>()
                .Ignore(x => x.Number)
                .Ignore(x => x.Year)
                .Ignore(x => x.StatusChangedOn)
                .Ignore(x => x.CustomerAddress)
                .Ignore(x => x.Source)
                .Ignore(x => x.ProductType)
                .Ignore(x => x.Notes)
                .Ignore(x => x.Quotations)
                .Ignore(x => x.Orders)
                .Ignore(x => x.Activities)
                .Ignore(x => x.Customer)
                .MapMember(x => x.CreatedById, y => y.OperatorId)
                .IgnoreCommonMembers();
           
            CreateMap<Job, JobDetailDto>()
                .MapMember(x => x.OperatorId, y => y.CreatedById);

            CreateMap<Job, JobDetailReadModel>()
                .MapMember(x=>x.Code,y=>y.Number+"/"+ y.Year)
                .MapMember(x=>x.CustomerName,y => y.Customer.CompanyName + " " + y.Customer.Surname + " " + y.Customer.Name)
                .MapMember(x=>x.CustomerFullAddress,y => y.CustomerAddress.StreetAddress + " - " + y.CustomerAddress.City)
                .MapMember(x => x.HasNotes, y => y.Notes.Count>0 ? y.Notes.FirstOrDefault().Attachments.Count > 0 : false)
                .MapMember(x => x.ActivityStart, y => y.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Start)
                .MapMember(x => x.ActivityEnd, y => y.Activities.OrderByDescending(z => z.End).FirstOrDefault().End)
                .MapMember(x => x.ActivityOperator, y => y.Activities.OrderByDescending(z => z.End).FirstOrDefault().Operator.Name + " " + y.Activities.OrderByDescending(z => z.End).FirstOrDefault().Operator.Surname)
                .MapMember(x => x.OperatorId, y => y.CreatedById);

            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<JobSource, JobSourceDto>();
            CreateMap<User, JobOperatorDto>();

            CreateMap<Job, JobSearchReadModel>()
                .MapMember(x => x.Code, y => y.Number + "/" + y.Year)
                .MapMember(x => x.CustomerName, y => y.Customer.CompanyName + " " + y.Customer.Surname + " " + y.Customer.Name)
                .MapMember(x => x.CustomerFullAddress, y => y.CustomerAddress.StreetAddress + " - " + y.CustomerAddress.City)
                .MapMember(x => x.LastQuotation, y => y.Quotations.OrderByDescending(z => z.CreatedOn).FirstOrDefault().Status)
                .MapMember(x => x.LastQuotationDate, y => y.Quotations.OrderByDescending(z => z.CreatedOn).FirstOrDefault().CreatedOn)
                .MapMember(x => x.LastOrder, y => y.Orders.OrderByDescending(z => z.CreatedOn).FirstOrDefault().Status)
                .MapMember(x => x.LastOrderDate, y => y.Orders.OrderByDescending(z => z.CreatedOn).FirstOrDefault().CreatedOn)
                .MapMember(x => x.LastActivity, y => y.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Status)
                .MapMember(x => x.LastActivityDate, y => y.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Start)
                .MapMember(x => x.LastActivityOperator, y => y.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Operator.Name + " " + y.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Operator.Surname)
                .Ignore(x => x.OperatorId);
        }
    }
}
