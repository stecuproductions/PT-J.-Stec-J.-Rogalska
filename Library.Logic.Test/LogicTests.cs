using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.API;
using Library.Logic.Implementation;
using Library.LogicTests.Mocks;
using System.Runtime.CompilerServices;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.LogicTests.Mocks;
namespace Library.LogicTests
{

    [TestClass]
    public class LogicTests
    {

        [TestMethod]
        public void AddingAndDeletingBookTest()
        {
            LibraryServiceWithMockRepo _libraryService = new LibraryServiceWithMockRepo(new MockRepo());
            string title = "Test Book";
            string author = "Test Author";
            bool result = _libraryService.AddBookLogic(title, author);
            Assert.IsTrue(result);
            var book = _libraryService.GetNBooksLogic(1000, 0).FirstOrDefault();
            Assert.IsNotNull(book);
            Assert.AreEqual(title, book.Title);
            Assert.AreEqual(author, book.Author);
            Assert.IsFalse(book.IsBorrowed);
            Guid bookId = book.Id;
            result = _libraryService.DeleteBookWithEventsLogic(bookId);
            Assert.IsTrue(result);
            var deletedBook = _libraryService.GetBookByIdLogic(bookId);
            Assert.IsNull(deletedBook);

        }
        [TestMethod]
        public void AddingUserTest()
        {
            LibraryServiceWithMockRepo _libraryService = new LibraryServiceWithMockRepo(new MockRepo());
            string name = "Test User";
            string surname = "Test Surname";
            bool result = _libraryService.AddUserLogic(name, surname);
            Assert.IsTrue(result);
            var user = _libraryService.GetNUsersLogic(1000, 0).FirstOrDefault();
            Assert.IsNotNull(user);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(surname, user.Surname);
            Guid userId = user.Id;
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void SearchThroughEventsTest()
        {
            LibraryServiceWithMockRepo _libraryService = new LibraryServiceWithMockRepo(new MockRepo());
            string title = "Test Book";
            string author = "Test Author";
            bool result = _libraryService.AddBookLogic(title, author);
            Assert.IsTrue(result);
            _libraryService.AddUserLogic("Test User", "Test Surname");
            var book = _libraryService.GetNBooksLogic(1000, 0).FirstOrDefault();
            Guid bookId = book.Id;
            var user = _libraryService.GetNUsersLogic(1000, 0).FirstOrDefault();
            Guid userId = user.Id;
            result = _libraryService.ReturnBookLogic(userId, bookId);
            Assert.IsFalse(result);
            result = _libraryService.BorrowBookLogic(userId, bookId);
            Assert.IsTrue(result);
            var borrow = _libraryService.GetNBorrowsLogic(1000, 0).FirstOrDefault();
            Assert.IsNotNull(borrow);
            Assert.AreEqual(userId, borrow.UserId);
            Assert.AreEqual(bookId, borrow.BookId);
            result = _libraryService.ReturnBookLogic(userId, bookId);
            var returned = _libraryService.GetNReturnsLogic(1000, 0).FirstOrDefault();
            Assert.IsNotNull(returned);
        }

    }
}
