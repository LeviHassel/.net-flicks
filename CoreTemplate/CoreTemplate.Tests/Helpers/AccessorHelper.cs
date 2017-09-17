using CoreTemplate.Accessors;
using CoreTemplate.Accessors.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;

namespace CoreTemplate.Tests.Helpers
{
    public class AccessorHelper : IDisposable
    {
        public CoreTemplateContext Context { get; private set; }

        private IDbContextTransaction _transaction;

        public AccessorHelper()
        {
            //TODO: Figure out how to specify test db here with the options var, otherwise this will ruin my main database
            var options = new DbContextOptions<CoreTemplateContext>();

            Context = new CoreTemplateContext(options);

            //This applies all pending migrations
            Context.Database.Migrate();

            _transaction = Context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    //Roll back the transaction, so the database is clean for the next test run
                    _transaction.Rollback();
                    _transaction.Dispose();
                }

                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }

        /*
         * TODO: Revise SeedMovies to look less like the original code and be more efficient
         */
        internal List<Movie> SeedMovies(int count = 1)
        {
            //Set up Fixture to populate random data
            Fixture fixture = new Fixture();

            var entities = new List<Movie>(count);

            for (int i = 0; i < count; i++)
            {
                entities.Add(fixture.Create<Movie>());
            }

            try
            {
                Context.Movies.AddRange(entities);
                Context.SaveChanges();

                //Detach created entities so that tests don't run into EF conflicts
                foreach (var e in entities)
                {
                    Context.Entry(e).State = EntityState.Detached;
                }

                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
