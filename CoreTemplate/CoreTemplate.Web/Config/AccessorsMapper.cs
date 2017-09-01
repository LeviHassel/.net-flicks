using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;

namespace CoreTemplate.Web.Config
{
    public class AutoMapperAccessorsConfig : Profile
    {
        public AutoMapperAccessorsConfig()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
