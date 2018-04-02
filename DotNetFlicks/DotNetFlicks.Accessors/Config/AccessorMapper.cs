using AutoMapper;
using DotNetFlicks.Accessors.Accessors;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using System;
using System.Linq;

namespace DotNetFlicks.Accessors.Config
{
    public class AccessorMapper : Profile
    {
        public AccessorMapper()
        {
            CreateMap<CastMember, CastMemberDTO>().ReverseMap();
            CreateMap<CrewMember, CrewMemberDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MovieGenre, MovieGenreDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<UserMovie, UserMovieDTO>().ReverseMap();

            CreateMap<RootObject, Movie>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.overview))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.release_date)))
                .ForMember(dest => dest.Runtime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.runtime)))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => "https://image.tmdb.org/t/p/w600_and_h900_bestv2/" + src.poster_path))
                .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => "https://www.youtube.com/watch?v=" + src.videos.results.First().key))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.genres))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.credits.cast))
                .ForMember(dest => dest.Crew, opt => opt.MapFrom(src => src.credits.crew));

            CreateMap<JsonGenre, MovieGenre>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<JsonCast, CastMember>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.character))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.order));

            CreateMap<JsonCrew, CrewMember>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.job))
                .ForMember(dest => dest.Department, opt => opt.Ignore());
                //.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.department)) //find a way to get Id from name
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
