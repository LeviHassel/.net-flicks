using Xunit;

namespace CoreTemplate.Tests.AccessorTests.Config
{
    [CollectionDefinition("Accessors")]
    public class AccessorCollection : ICollectionFixture<AccessorHelper>
    {
        // This class has no code, and is never created. Its purpose is simply to be the
        //place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.
    }
}
