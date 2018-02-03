using AutoMapper;
using CoreTemplate.Managers.Config;
using System;

namespace CoreTemplate.Tests.ManagerTests.Config
{
    public class ManagerFixture : IDisposable
    {
        //The purpose of this Collection Fixture (no relation to AutoFixture) is to create a single test context, share
        //it among all Manager tests, and have it cleaned up after all the tests in the test classes have finished
        //https://xunit.github.io/docs/shared-context.html#collection-fixture
        public ManagerFixture()
        {
            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<ManagerMapper>();
            });
        }

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}