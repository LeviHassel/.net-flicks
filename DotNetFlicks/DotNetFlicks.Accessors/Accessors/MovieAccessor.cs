using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TMDbLib.Client;

namespace DotNetFlicks.Accessors.Accessors
{
    public class MovieAccessor : EntityAccessor<Entity>, IMovieAccessor
    {
        public MovieAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public MovieDTO Get(int id)
        {
            var movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(@"Database" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "movies.json"));

            var peopleIds = movies.SelectMany(x => x.Cast.Select(y => y.PersonId)).ToList();
            peopleIds.AddRange(movies.SelectMany(x => x.Crew.Select(y => y.PersonId)).ToList());
            peopleIds = peopleIds.Distinct().OrderBy(x => x).ToList();

            var people = new List<Person>();
            TMDbClient client = new TMDbClient("5955178fb6a488b7ec2f4bd861ed3e33");
            

            foreach (var personId in peopleIds)
            {
                //Only 40 requests per 10 seconds, so make it slow somehow
                //Get all 4890 people details using TMDBlib
                var personData = client.GetPersonAsync(personId).Result;

                people.Add(
                    new Person
                    {
                        Id = personData.Id,
                        Name = personData.Name,
                        Biography = personData.Biography,
                        //BirthDate = personData.Birthday.HasValue ? personData.Birthday.Value : null,
                        //DeathDate = personData.Deathday.HasValue ? personData.Deathday.Value : null,
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/" + personData.ProfilePath
                    }
                );

                Thread.Sleep(500);
            }

            var peopleJson = JsonConvert.SerializeObject(people);

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
