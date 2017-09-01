using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.ViewModels.Movie;

namespace CoreTemplate.Web.Config
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
