using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Interfaces;
using Library.Data;

namespace LibraryTests
{
    [TestClass]
    public class SqlDataProviderTests
    {
        private IDataProvider provider;

        [TestInitialize]
        public void Setup() { 
        
            provider = DataProviderFactory.CreateSqlDataProvider();
        }
        [TestMethod]
        public void AddAndDeleteUserTest()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
            };
            var newEvent = provider.AddUser(newUser.Name, newUser.Id);
            var state = provider.GetLibraryState();
            var user = state.Users.FirstOrDefault(u => u.Name == newUser.Name);
            Assert.IsNotNull(user);
            var events = provider.GetEvents();
            var matchingEvent = events.FirstOrDefault(e => e.Id == newEvent.Id);
            Assert.IsNotNull(matchingEvent);
            Assert.AreEqual($"User {newUser.Name} {newUser.Id} added", matchingEvent.Description);
        }
        [TestMethod]
        public void AddBookBorrowItAndReturnTest()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
            };
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                IsBorrowed = false,
            };
            provider.AddUser(newUser.Name, newUser.Id);
            provider.AddBook(newBook.Title, newBook.Author, newBook.Id);

            var borrowEvent = provider.BorrowBook(newUser, newBook);
            var state = provider.GetLibraryState()
                ;
            var book = state.Books.FirstOrDefault(b => b.Id == newBook.Id);
            Assert.IsNotNull(book);
            Assert.IsTrue(book.IsBorrowed);
            var events = provider.GetEvents();
            var matchingEvent = events.FirstOrDefault(e => e.Id == borrowEvent.Id);
            Assert.IsNotNull(matchingEvent);
            Assert.AreEqual($"User {newUser.Name} {newUser.Id} borrowed book {newBook.Title} {newBook.Id}", matchingEvent.Description);
            var returnEvent = provider.ReturnBook(newUser, newBook);
            book = provider.GetLibraryState().Books.FirstOrDefault(b => b.Id == newBook.Id);
            Assert.IsNotNull(book);
            Assert.IsFalse(book.IsBorrowed);
        }
        [TestMethod]
        public void RemovingBooksTest()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
            };
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                IsBorrowed = false,
            };
            provider.AddUser(user.Name, user.Id);
            provider.AddBook(book.Title, book.Author, book.Id);
            var borrowEvent = provider.BorrowBook(user, book); //ten evt powinien zniknac po usunieciu
            provider.RemoveBook(book);
            var state = provider.GetLibraryState();
            var removedBook = state.Books.FirstOrDefault(b => b.Id == book.Id);
            Assert.IsNull(removedBook);
            var events = provider.GetEvents();
            var matchingEvent = events.FirstOrDefault(e => e.Id == borrowEvent.Id);
            Assert.IsNull(matchingEvent);

        }

        [TestMethod]
        public void RemovingUsersTest()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
            };
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                IsBorrowed = false,
            };
            provider.AddUser(user.Name, user.Id);
            provider.AddBook(book.Title, book.Author, book.Id);
            var borrowEvent = provider.BorrowBook(user, book); //ten evt powinien zniknac po usunieciu
            provider.RemoveUser(user);
            var state = provider.GetLibraryState();
            var removedUser = state.Users.FirstOrDefault(u => u.Id == user.Id);
            Assert.IsNull(removedUser);
            var events = provider.GetEvents();
            var matchingEvent = events.FirstOrDefault(e => e.Id == borrowEvent.Id);
            Assert.IsNull(matchingEvent);
        }
    }

}