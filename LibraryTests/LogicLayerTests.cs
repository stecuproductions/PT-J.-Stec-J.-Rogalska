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
    public sealed class LogicLayerTests
    {
        [TestMethod]
        public void BorrowBookTest()
        {
            var dataProvider = new InMemoryDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book = state.Books.First();
            var service = new LibraryService(dataProvider);
            var result = service.BorrowBook(user.Id, book.Id);
            Assert.IsTrue(result);
            Assert.IsTrue(book.IsBorrowed);
            Assert.AreEqual(1, user.BorrowedBooks.Count);
        }

        [TestMethod]
        public void ReturnBookTest()
        {
            var dataProvider = new InMemoryDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book = state.Books.First();
            var service = new LibraryService(dataProvider);
            service.BorrowBook(user.Id, book.Id);
            var result = service.ReturnBook(user.Id, book.Id);
            Assert.IsTrue(result);
            Assert.IsFalse(book.IsBorrowed);
            Assert.AreEqual(0, user.BorrowedBooks.Count);
        }

        [TestMethod]
        public void GetAvailableBooksTest_AllBooksAvailable()
        {
            var dataProvider = new InMemoryDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book1 = state.Books.First();
            var book2 = state.Books.Last();
            var service = new LibraryService(dataProvider);
            var result = service.GetAvailableBooks();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAvailableBooksTest_AllBooksBorrowed()
        {
            var dataProvider = new InMemoryDataProvider();
            dataProvider.GenerateSampleData();
            var state = dataProvider.GetLibraryState();
            var user = state.Users.First();
            var book1 = state.Books.First();
            var book2 = state.Books.Last();
            var service = new LibraryService(dataProvider);
            service.BorrowBook(user.Id, book1.Id);
            service.BorrowBook(user.Id, book2.Id);
            var result = service.GetAvailableBooks();
            Assert.AreEqual(0, result.Count);
        }
    }
}
