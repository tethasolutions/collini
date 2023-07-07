using AutoMapper;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        //CreateMap<ContactDto, Contact>()
        //    .IgnoreCommonMembers()
        //    .Ignore(x => x.Jobs)
        //    .Ignore(x => x.Orders)
        //    .ReverseMap();

        //CreateMap<AddressDto, ContactAddress>()
        //    .IgnoreCommonMembers()
        //    .Ignore(x => x.Description)
        //    .Ignore(x => x.Contact)
        //    .Ignore(x => x.Jobs)
        //    .ReverseMap();
    }
}