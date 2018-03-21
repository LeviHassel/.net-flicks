using AutoMapper;
using DotNetFlicks.Accessors.Config;
using System;

namespace DotNetFlicks.Tests.AccessorTests.Config
{
    public class AccessorFixture : IDisposable
    {
        //The purpose of this Collection Fixture (no relation to AutoFixture) is to create a single test context, share
        //it among all Accessor tests, and have it cleaned up after all the tests in the test classes have finished
        //https://xunit.github.io/docs/shared-context.html#collection-fixture
        public AccessorFixture()
        {
            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<AccessorMapper>();
            });
        }

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}