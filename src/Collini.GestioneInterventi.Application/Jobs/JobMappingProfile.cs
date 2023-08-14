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
                .IgnoreCommonMembers();
           
            CreateMap<Job, JobDetailDto>()
                .Ignore(x=>x.OperatorId);

            CreateMap<Job, JobDetailReadModel>()
                .MapMember(x=>x.Code,y=>y.Number+"/"+ y.Year)
                .MapMember(x=>x.CustomerName,y => y.Customer.CompanyName + " " + y.Customer.Surname + " " + y.Customer.Name)
                .MapMember(x=>x.CustomerFullAddress,y => y.CustomerAddress.StreetAddress + " - " + y.CustomerAddress.City + " - " + y.CustomerAddress.Province)
                .Ignore(x => x.OperatorId);

            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<JobSource, JobSourceDto>();
            CreateMap<User, JobOperatorDto>();

            CreateMap<Job, JobSearchReadModel>()
                .MapMember(x => x.Code, y => y.Number + "/" + y.Year)
                .MapMember(x => x.CustomerName, y => y.Customer.CompanyName + " " + y.Customer.Surname + " " + y.Customer.Name)
                .MapMember(x => x.CustomerFullAddress, y => y.CustomerAddress.StreetAddress + " - " + y.CustomerAddress.City + " - " + y.CustomerAddress.Province)
                .MapMember(x => x.LastDocument, y => y.Orders.Where(z=> z.JobId == y.Id).OrderByDescending(z => z.CreatedOn).Take(1).FirstOrDefault().Status)
                .MapMember(x => x.LastDocumentDate, y => y.Activities.Where(z => z.JobId == y.Id).OrderByDescending(z => z.CreatedOn).FirstOrDefault().CreatedOn.ToUniversalTime())
                .Ignore(x => x.OperatorId);
        }
    }
}
