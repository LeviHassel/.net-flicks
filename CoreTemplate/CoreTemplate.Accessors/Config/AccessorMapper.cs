using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;

namespace CoreTemplate.Accessors.Config
{
    public class AccessorMapper : Profile
    {
        public AccessorMapper()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
