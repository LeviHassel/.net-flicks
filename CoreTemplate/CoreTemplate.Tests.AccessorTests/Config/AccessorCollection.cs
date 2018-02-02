using Xunit;

namespace CoreTemplate.Tests.AccessorTests.Config
{
    [CollectionDefinition("Accessors")]
    public class AccessorCollection : ICollectionFixture<AccessorStarter>
    {
        // This class has no code, and is never created. Its purpose is simply to be the
        //place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.
        //https://xunit.github.io/docs/shared-context.html#collection-fixture
    }
}
