using AutoMapper;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.ViewModels.Department;
using DotNetFlicks.ViewModels.Genre;
using DotNetFlicks.ViewModels.Movie;
using DotNetFlicks.ViewModels.Person;
using DotNetFlicks.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Managers.Config
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<CastMemberDTO, CastMemberViewModel>().ReverseMap();

            CreateMap<CastMemberDTO, MovieRoleViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
                .ForMember(dest => dest.MovieYear, opt => opt.MapFrom(src => src.Movie.ReleaseDate.Year))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => "Acting"));

            CreateMap<CrewMemberDTO, CrewMemberViewModel>().ReverseMap();

            CreateMap<CrewMemberDTO, MovieRoleViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
                .ForMember(dest => dest.MovieYear, opt => opt.MapFrom(src => src.Movie.ReleaseDate.Year))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.IsDirector, opt => opt.MapFrom(src => src.Department.IsDirecting));

            CreateMap<DepartmentDTO, DepartmentViewModel>().ReverseMap();

            CreateMap<GenreDTO, GenreViewModel>()
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(x => x.Movie)));

            CreateMap<GenreViewModel, GenreDTO>();

            CreateMap<MovieDTO, MovieViewModel>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => x.Genre)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Replace("\\n", System.Environment.NewLine)));

            CreateMap<MovieViewModel, MovieDTO>();

            CreateMap<MovieDTO, EditMovieViewModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Replace("\\n", System.Environment.NewLine)));

            CreateMap<EditMovieViewModel, MovieDTO>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Replace(System.Environment.NewLine, "\\n")))
                .ForMember(dest => dest.Cast, opt => opt.Ignore())
                .ForMember(dest => dest.Crew, opt => opt.Ignore());

            CreateMap<PersonDTO, PersonViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => Mapper.Map<List<MovieRoleViewModel>>(src.CastRoles).Concat(Mapper.Map<List<MovieRoleViewModel>>(src.CrewRoles)).ToList()))
                .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography.Replace("\\n", System.Environment.NewLine)));

            CreateMap<PersonViewModel, PersonDTO>()
                .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography.Replace(System.Environment.NewLine, "\\n")));
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
