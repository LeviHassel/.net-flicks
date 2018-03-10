using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.ViewModels.Genre;
using CoreTemplate.ViewModels.Department;
using CoreTemplate.ViewModels.Movie;
using CoreTemplate.ViewModels.Person;

namespace CoreTemplate.Managers.Config
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<DepartmentDTO, DepartmentViewModel>().ReverseMap();
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
            CreateMap<CrewMemberDTO, CrewMemberViewModel>().ReverseMap();
            CreateMap<PersonDTO, PersonViewModel>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
