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

namespace Library.LogicTests
{

    [TestClass]
    public class LogicTests
    {
        private ILibraryService _libraryService = new LibraryServiceWithMockRepo(new MockRepo());
        
        [TestMethod]
        public void AddingAndDeletingBookTest()
        {
            string title = "Test Book";
            string author = "Test Author";
            bool result = _libraryService.AddBookLogic(title, author);
            Assert.IsTrue(result);
            var book = _libraryService.GetNBooksLogic(1, 0).FirstOrDefault();
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

    }
}
