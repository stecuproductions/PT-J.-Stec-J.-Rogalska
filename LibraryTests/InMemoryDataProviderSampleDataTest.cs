using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;
using Library.Logic;

namespace LibraryTests
{
    [TestClass]
    public sealed class InMemoryDataProviderSampleDataTest
    {
        [TestMethod]
        public void BorrowBookTest()
        {
            var dataProvider = new SampleDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book = state.Books.First();
            var service = new LibraryService(dataProvider);
            var result = service.BorrowBook(user.Id, book.Id);
            Assert.IsTrue(result);
            Assert.IsTrue(book.IsBorrowed);
        }

        [TestMethod]
        public void ReturnBookTest()
        {
            var dataProvider = new SampleDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book = state.Books.First();
            var service = new LibraryService(dataProvider);
            service.BorrowBook(user.Id, book.Id);
            var result = service.ReturnBook(user.Id, book.Id);
            Assert.IsTrue(result);
            Assert.IsFalse(book.IsBorrowed);
        }

        [TestMethod]
        public void GetAvailableBooksTest_AllBooksAvailable()
        {
            var dataProvider = new SampleDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book1 = state.Books.First();
            var book2 = state.Books.Last();
            var service = new LibraryService(dataProvider);

        }

    }
}
