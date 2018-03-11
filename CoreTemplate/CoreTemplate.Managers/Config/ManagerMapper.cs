using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.ViewModels.Genre;
using CoreTemplate.ViewModels.Department;
using CoreTemplate.ViewModels.Movie;
using CoreTemplate.ViewModels.Person;
using CoreTemplate.ViewModels.Shared;
using System.Linq;
using CoreTemplate.Common.Helpers;
using System.Collections.Generic;

namespace CoreTemplate.Managers.Config
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<CastMemberDTO, CastMemberViewModel>().ReverseMap();

            CreateMap<CastMemberDTO, MovieRoleViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
                .ForMember(dest => dest.MovieYear, opt => opt.MapFrom(src => src.Movie.ReleaseDate.Year))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => "Acting"));

            CreateMap<CrewMemberDTO, CrewMemberViewModel>().ReverseMap();

            CreateMap<CrewMemberDTO, MovieRoleViewModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
                .ForMember(dest => dest.MovieYear, opt => opt.MapFrom(src => src.Movie.ReleaseDate.Year))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<DepartmentDTO, DepartmentViewModel>().ReverseMap();

            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();

            CreateMap<MovieDTO, MovieViewModel>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => x.Genre)))
                .ForMember(dest => dest.GenresTooltip, opt => opt.MapFrom(src => ListHelper.GetTooltipList(src.Genres.Select(x => x.Genre.Name).OrderBy(y => y).ToList())));

            CreateMap<MovieViewModel, MovieDTO>();

            CreateMap<MovieDTO, EditMovieViewModel>();

            CreateMap<EditMovieViewModel, MovieDTO>()
                .ForMember(dest => dest.Cast, opt => opt.Ignore())
                .ForMember(dest => dest.Crew, opt => opt.Ignore());

            CreateMap<PersonDTO, PersonViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => Mapper.Map<List<MovieRoleViewModel>>(src.CastRoles).Concat(Mapper.Map<List<MovieRoleViewModel>>(src.CrewRoles)).ToList()));

            CreateMap<PersonViewModel, PersonDTO>();
        }

        public override string ProfileName
        {
            get { return GetType().ToString(); }
        }
    }
}
