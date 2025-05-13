using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic;

namespace LibraryTests.LogicTests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void TestAddBook()
        {
            ILibraryService libraryService = new LibraryService(new InMemoryDataProvider());

            string title = "Test Book";
            string author = "Test Author";
            // Act
            bool result = libraryService.AddBook(title, author);
            // Assert
            Assert.IsTrue(result);
            var books = libraryService.GetBooks();
            Assert.AreEqual(1, books.Count);
            Assert.AreEqual(title, books[0].Title);
        }
        [TestMethod]
        public void TestAddUser()
        {
            ILibraryService libraryService = new LibraryService(new InMemoryDataProvider());
            string name = "Test User";
            bool result = libraryService.AddUser(name);
            Assert.IsTrue(result);
            var users = libraryService.GetUsers();
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual(name, users[0].Name);
        }
        [TestMethod]
        public void TestRemoveBook()
        {
            ILibraryService libraryService = new LibraryService(new InMemoryDataProvider());
            string title = "Test Book";
            string author = "Test Author";
            libraryService.AddBook(title, author);
            var booksBefore = libraryService.GetBooks();
            Assert.AreEqual(1, booksBefore.Count);
            var bookId = booksBefore[0].Id;
            bool result = libraryService.RemoveBook(bookId);
            Assert.IsTrue(result);
            var booksAfter = libraryService.GetBooks();
            Assert.AreEqual(0, booksAfter.Count);
        }
        [TestMethod]
        public void TestRemoveUser()
        {
            ILibraryService libraryService = new LibraryService(new InMemoryDataProvider());
            string name = "Test User";
            libraryService.AddUser(name);
            var usersBefore = libraryService.GetUsers();
            Assert.AreEqual(1, usersBefore.Count);
            var userId = usersBefore[0].Id;
            bool result = libraryService.RemoveUser(userId);
            Assert.IsTrue(result);
            var usersAfter = libraryService.GetUsers();
            Assert.AreEqual(0, usersAfter.Count);
        }
        [TestMethod]
        public void TestBorrowBook()
        {
            ILibraryService libraryService = new LibraryService(new InMemoryDataProvider());
            string title = "Test Book";
            string author = "Test Author";
            libraryService.AddBook(title, author);
            string userName = "Test User";
            libraryService.AddUser(userName);
            var booksBefore = libraryService.GetBooks();
            var usersBefore = libraryService.GetUsers();
            Assert.AreEqual(1, booksBefore.Count);
            Assert.AreEqual(1, usersBefore.Count);
            var bookId = booksBefore[0].Id;
            var userId = usersBefore[0].Id;
            bool result = libraryService.BorrowBook(userId, bookId);
            Assert.IsTrue(result);
            var booksAfter = libraryService.GetBooks();
            Assert.AreEqual(1, booksAfter.Count);
            Assert.IsTrue(booksAfter[0].IsBorrowed);
        }
        [TestMethod]
        public void TestReturnBook()
        {
            ILibraryService libraryService = new LibraryService(new InMemoryDataProvider());
            string title = "Test Book";
            string author = "Test Author";
            libraryService.AddBook(title, author);
            string userName = "Test User";
            libraryService.AddUser(userName);
            var booksBefore = libraryService.GetBooks();
            var usersBefore = libraryService.GetUsers();
            Assert.AreEqual(1, booksBefore.Count);
            Assert.AreEqual(1, usersBefore.Count);
            var bookId = booksBefore[0].Id;
            var userId = usersBefore[0].Id;
            libraryService.BorrowBook(userId, bookId);
            bool result = libraryService.ReturnBook(userId, bookId);
            Assert.IsTrue(result);
            var booksAfter = libraryService.GetBooks();
            Assert.AreEqual(1, booksAfter.Count);
            Assert.IsFalse(booksAfter[0].IsBorrowed);
        }

    }
}
