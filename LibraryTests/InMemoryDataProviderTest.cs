using Library.Data;
namespace LibraryTests

{
    [TestClass]
    public sealed class LibraryDataLayerTests
    {
        [TestMethod]
        public void InMemoryDataProviderTests()
        {
            var provider = new SampleDataProvider();
            provider.GenerateSampleData();
            var state = provider.GetLibraryState();
            Assert.IsNotNull(state);
            Assert.IsNotNull(state.Books);
            Assert.IsNotNull(state.Users);
            Assert.IsTrue(state.Books.Count > 0);
            Assert.IsTrue(state.Users.Count > 0);
            Assert.AreEqual(1, state.Users.Count);
            Assert.AreEqual(2, state.Books.Count);
            Assert.AreEqual("Wiedźmin", state.Books[0].Title);
            Assert.AreEqual("Andrzej Sapkowski", state.Books[0].Author);
        }
        [TestMethod]
        public void InMemoryDataProviderEmptyDataSetTest()
        {
            var provider = new SampleDataProvider();
            provider.GenerateSampleData();
            provider.GenerateEmptyDataSet();
            var state = provider.GetLibraryState();
            Assert.IsNotNull(state);
            Assert.IsNotNull(state.Books);
            Assert.IsNotNull(state.Users);
            Assert.AreEqual(0, state.Books.Count);
            Assert.AreEqual(0, state.Users.Count);
        }
    }

}
