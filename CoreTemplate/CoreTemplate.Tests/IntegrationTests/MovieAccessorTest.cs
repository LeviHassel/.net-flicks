using AutoMapper;
using CoreTemplate.Accessors.Accessors;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;
using CoreTemplate.Tests.Helpers;
using System;
using System.Linq;
using Xunit;

/*
 * TODO: Revise all of these functions to be my own; think of additional things I can test or more efficient ways to test them.
 *       Also there may be a much better way to seed movies and build my accessor helper using AutoFixture.
 */

namespace CoreTemplate.Tests.IntegrationTests
{
    public class MovieAccessorTest
    {
        private IMovieAccessor _movieAccessor;
        private AccessorHelper _accessorHelper;

        public MovieAccessorTest()
        {
            _accessorHelper = new AccessorHelper();
            _movieAccessor = new MovieAccessor(_accessorHelper.Context);

            Mapper.Initialize(x =>
            {
                x.CreateMap<Movie, MovieDTO>().ReverseMap();
            });
        }

        [Fact]
        public void MovieAccessor_Get()
        {
            //Seed an entry
            var entity = _accessorHelper.SeedMovies().First();

            //Get the entry
            var dtoDb = _movieAccessor.Get(entity.Id);

            var entityDb = Mapper.Map<Movie>(dtoDb);

            //Ensure that the database returns what we gave it
            Assert.True(PropertyComparer.Equal(entity, entityDb));
        }

        [Fact]
        public void MovieAccessor_GetAll()
        {
            //Seed mutliple entries
            var entities = _accessorHelper.SeedMovies(10);

            //Get all of the entries
            var dtosDb = _movieAccessor.GetAll();

            //Ensure that the database returned something
            Assert.NotNull(dtosDb);

            //Ensure that the database returned the correct amount of entities
            Assert.Equal(entities.Count, dtosDb.Count);

            //Ensure that every entity's individual properties match
            foreach (var entity in entities)
            {
                var dtoDb = dtosDb.Single(x => x.Id == entity.Id);

                var entityDb = Mapper.Map<Movie>(dtoDb);

                Assert.True(PropertyComparer.Equal(entity, entityDb));
            }
        }

        [Fact]
        public void MovieAccessor_Save()
        {
            //Seed an entry
            var entity = _accessorHelper.SeedMovies().First();

            //Keep a local copy of the entry's name
            var originalName = entity.Name;

            //Change the name
            entity.Name = Guid.NewGuid().ToString();

            //Keep a local copy of the new name
            var newName = entity.Name;

            //Save the entry
            var dto = Mapper.Map<MovieDTO>(entity);
            _movieAccessor.Save(dto);

            //Get the entry from the database
            var entityDb = _accessorHelper.Context.Movies.Single(x => x.Id == entity.Id);

            //Ensure that the name in the database is not the original name
            Assert.NotEqual(originalName, entityDb.Name);

            //Ensure that the name in the database is the new name
            Assert.Equal(newName, entityDb.Name);

            //Ensure that the database returns what we gave it
            Assert.True(PropertyComparer.Equal(entity, entityDb));
        }
    }
}
