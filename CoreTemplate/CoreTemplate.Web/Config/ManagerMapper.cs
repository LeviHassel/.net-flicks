using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.ViewModels.Movie;

namespace CoreTemplate.Web.Config
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
