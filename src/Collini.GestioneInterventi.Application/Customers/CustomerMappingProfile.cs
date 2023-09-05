using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Customers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>()
                .Ignore(x=>x.Jobs)
                .Ignore(x=>x.Orders)
                .IgnoreCommonMembers();

            CreateMap<ContactAddress, AddressDto>();
            CreateMap<AddressDto, ContactAddress>()
                .Ignore(x=>x.Description)
                .Ignore(x=>x.Contact)
                .Ignore(x=>x.Jobs)
                .IgnoreCommonMembers();
        }
    }
}
