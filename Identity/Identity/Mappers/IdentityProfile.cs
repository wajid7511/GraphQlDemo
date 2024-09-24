using AutoMapper;
using Identity.Abstractions.Models;
using Identity.Models;

namespace Identity.Mappers;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<IdentityCreateDto, IdentityCreateResponseModel>();
    }
}
