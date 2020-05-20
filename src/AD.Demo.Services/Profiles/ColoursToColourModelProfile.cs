using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using AutoMapper;

namespace AD.Demo.Services.Profiles
{
    public class ColoursToColourModelProfile : Profile
    {
        public ColoursToColourModelProfile()
        {
            CreateMap<Colours, ColourModel>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(c => c.ColourId));
        }
    }
}