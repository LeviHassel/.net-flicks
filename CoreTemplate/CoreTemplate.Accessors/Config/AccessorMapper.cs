using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;

namespace CoreTemplate.Accessors.Config
{
    public class AccessorMapper : Profile
    {
        public AccessorMapper()
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MovieGenre, MovieGenreDTO>().ReverseMap();
            CreateMap<MoviePerson, MoviePersonDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<UserMovie, UserMovieDTO>().ReverseMap();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
