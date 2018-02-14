using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.ViewModels.Genre;
using CoreTemplate.ViewModels.Job;
using CoreTemplate.ViewModels.Movie;
using CoreTemplate.ViewModels.Person;

namespace CoreTemplate.Managers.Config
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<JobDTO, JobViewModel>().ReverseMap();
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
            CreateMap<MoviePersonDTO, MoviePersonViewModel>().ReverseMap();
            CreateMap<PersonDTO, PersonViewModel>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
