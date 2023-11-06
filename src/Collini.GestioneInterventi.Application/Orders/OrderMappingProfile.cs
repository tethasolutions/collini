using AutoMapper;
using Collini.GestioneInterventi.Application.Notes.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Orders.DTOs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Orders
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDetailDto>()
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDate, y => y.Job.JobDate)
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name +
                    ((y.Job.Customer.Telephone != null) ? " - Tel: " + y.Job.Customer.Telephone : "") +
                    ((y.Job.Customer.Email != null) ? " - Email: " + y.Job.Customer.Email : ""))
                .MapMember(x => x.SupplierName, y => y.Supplier.CompanyName +
                    ((y.Supplier.Telephone != null) ? " - Tel: " + y.Supplier.Telephone : "") +
                    ((y.Supplier.Email != null) ? " - Email: " + y.Supplier.Email : ""))
                .MapMember(x => x.HasNotes, y => y.Job.Notes.Count > 0 ? y.Job.Notes.FirstOrDefault().Attachments.Count > 0 : false);

            CreateMap<OrderDetailDto,Order >()
                .Ignore(x=>x.Supplier)
                .Ignore(x=>x.StatusChangedOn)
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Notes)
                .IgnoreCommonMembers();

            CreateMap<Order, OrderReadModel>()
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDate, y => y.Job.JobDate)
                .MapMember(x => x.JobDescription, y => y.Job.Description)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name +
                    ((y.Job.Customer.Telephone != null) ? " - Tel: " + y.Job.Customer.Telephone : "") +
                    ((y.Job.Customer.Email != null) ? " - Email: " + y.Job.Customer.Email : ""))
                .MapMember(x => x.SupplierName, y => y.Supplier.CompanyName +
                    ((y.Supplier.Telephone != null) ? " - Tel: " + y.Supplier.Telephone : "") +
                    ((y.Supplier.Email != null) ? " - Email: " + y.Supplier.Email : ""));
        }

    }
}
