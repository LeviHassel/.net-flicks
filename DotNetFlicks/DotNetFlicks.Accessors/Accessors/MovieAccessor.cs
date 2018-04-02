using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetFlicks.Accessors.Accessors
{
    internal class BelongsToCollection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }

    internal class JsonGenre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    internal class ProductionCompany
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    internal class ProductionCountry
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    internal class SpokenLanguage
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

    internal class Result
    {
        public string id { get; set; }
        public string iso_639_1 { get; set; }
        public string iso_3166_1 { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }

    internal class Videos
    {
        public List<Result> results { get; set; }
    }

    internal class JsonCast
    {
        public int cast_id { get; set; }
        public string character { get; set; }
        public string credit_id { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public string profile_path { get; set; }
    }

    internal class JsonCrew
    {
        public string credit_id { get; set; }
        public string department { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string job { get; set; }
        public string name { get; set; }
        public string profile_path { get; set; }
    }

    internal class Credits
    {
        public List<JsonCast> cast { get; set; }
        public List<JsonCrew> crew { get; set; }
    }

    internal class RootObject
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public BelongsToCollection belongs_to_collection { get; set; }
        public int budget { get; set; }
        public List<JsonGenre> genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public Videos videos { get; set; }
        public Credits credits { get; set; }
    }


    public class MovieAccessor : EntityAccessor<Entity>, IMovieAccessor
    {
        public MovieAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public MovieDTO Get(int id)
        {
            var genres = JsonConvert.DeserializeObject<List<Genre>>(File.ReadAllText(@"Database" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "genres.json"));

            var jsonMovies = JsonConvert.DeserializeObject<List<RootObject>>(File.ReadAllText(@"Database" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "movies.json"));
            var movies = Mapper.Map<List<Movie>>(jsonMovies);

            foreach (var movie in movies)
            {
                foreach (var moviegenre in movie.Genres)
                {
                    moviegenre.MovieId = movie.Id;
                }

                foreach (var castmember in movie.Cast)
                {
                    castmember.MovieId = movie.Id;
                }

                foreach (var crewmember in movie.Crew)
                {
                    crewmember.MovieId = movie.Id;
                }
            }

            //Add purchase cost and Rent cost to objects

            //Find a way to populate Department ID for Crew

            var peopleIds = movies.SelectMany(x => x.Cast.Select(y => y.PersonId)).ToList();
            peopleIds.AddRange(movies.SelectMany(x => x.Crew.Select(y => y.PersonId)).ToList());
            peopleIds = peopleIds.Distinct().OrderBy(x => x).ToList();

            var people = new List<Person>();

            foreach (var personId in peopleIds)
            {
                //Only 40 requests per 10 seconds, so make it slow somehow
                //Get all 4890 people details using TMDBlib

                people.Add(
                    new Person
                    {
                        FirstName = ""
                    }
                );
            }

            var entity = _db.Movies
                .Include(x => x.Cast).ThenInclude(x => x.Person)
                .Include(x => x.Crew).ThenInclude(x => x.Person)
                .Include(x => x.Crew).ThenInclude(x => x.Department)
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Single(x => x.Id == id);

            var dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }

        public List<MovieDTO> GetAll()
        {
            var entities = _db.Movies
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .ToList();

            var dtos = Mapper.Map<List<MovieDTO>>(entities);

            return dtos;
        }

        public MovieDTO Save(MovieDTO dto)
        {
            var entity = Mapper.Map<Movie>(dto);

            _db.Movies.Update(entity);
            _db.SaveChanges();

            dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }

        public MovieDTO Delete(int id)
        {
            var entity = _db.Movies.Single(x => x.Id == id);

            _db.Movies.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }
    }
}
