using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.ViewModels.Home;

namespace CoreTemplate.Managers.Config
{
    public class AutoMapperManagersConfig : Profile
    {
        public AutoMapperManagersConfig()
        {
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
