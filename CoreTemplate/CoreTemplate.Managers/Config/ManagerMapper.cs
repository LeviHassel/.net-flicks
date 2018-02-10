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
            //TODO: Remove mappings I don't need
            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<JobDTO, JobViewModel>().ReverseMap();
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
            //CreateMap<MovieGenreDTO, MovieGenreViewModel>().ReverseMap();
            //CreateMap<MoviePersonDTO, MoviePersonViewModel>().ReverseMap();
            CreateMap<PersonDTO, PersonViewModel>().ReverseMap();
            //CreateMap<UserMovieDTO, UserMovieViewModel>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
