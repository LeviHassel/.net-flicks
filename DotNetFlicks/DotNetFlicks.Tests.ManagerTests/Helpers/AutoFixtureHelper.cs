using AutoFixture;
using AutoFixture.AutoMoq;
using System.Linq;

namespace DotNetFlicks.Tests.ManagerTests.Helpers
{
    public static class AutoFixtureHelper
    {
        /// <summary>
        /// Creates a fixture for populating random data that is customized to use AutoMoq and ignore circular references
        /// </summary>
        /// <returns></returns>
        public static IFixture CreateFixture()
        {
            //Set up a Fixture to populate random data: https://github.com/AutoFixture/AutoFixture
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Allow for circular references: https://github.com/AutoFixture/AutoFixture/wiki/Examples-of-using-behaviors
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                 .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}