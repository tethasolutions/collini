using AutoMapper;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Security;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<UserDto, User>()
            .IgnoreCommonMembers()
            .Ignore(x => x.AccessToken)
            .Ignore(x => x.PasswordHash)
            .Ignore(x => x.Salt)
            .Ignore(x => x.Activities);

        CreateMap<User, UserReadModel>();
    }
}