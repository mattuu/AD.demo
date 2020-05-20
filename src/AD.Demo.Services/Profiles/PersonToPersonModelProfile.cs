using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using AutoMapper;

namespace AD.Demo.Services.Profiles
{
    public class PersonToPersonModelProfile : Profile
    {
        public PersonToPersonModelProfile()
        {
            CreateMap<People, PersonModel>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(p => p.PersonId));
        }
    }
}