using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Quotations
{
    public class QuotationMapperProfile:Profile
    {
        public QuotationMapperProfile()
        {
            CreateMap<Quotation, QuotationDetailDto>()
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.JobDate, y => y.Job.CreatedOn)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name);

            CreateMap<QuotationDetailDto, Quotation>()
                .Ignore(x=>x.StatusChangedOn)
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Notes)
                .IgnoreCommonMembers();

            CreateMap<Quotation, QuotationReadModel>()
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name);


        }
    }
}
