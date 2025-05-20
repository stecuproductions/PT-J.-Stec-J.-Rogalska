using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;
namespace Library.DataTests
{
    [TestClass]
    [DoNotParallelize]
    public class DataRepositoryTest
    {
        private const string defaultConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=libraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private  ClearableDataRepository dataProvider;
        [TestInitialize]
        public void Setup()
        {
            dataProvider = new ClearableDataRepository(defaultConnectionString);
            dataProvider.ClearDatabase();
        }
        [TestMethod]
        public void TestAddBook()
        {
            Guid newBookId = Guid.NewGuid();
            dataProvider.AddBook(newBookId, "Test Book", "Test Author", false);
            var book = dataProvider.GetBookById(newBookId);
            Assert.IsNotNull(book);
            Assert.AreEqual("Test Book", book.Title);
            Assert.AreEqual("Test Author", book.Author);
            Assert.IsFalse(book.IsBorrowed);
            List<IBook> books = dataProvider.GetNBooks(1000, 0);
            Assert.IsNotNull(books);
            IBook currentBook = books.FirstOrDefault(b => b.Id == newBookId);
            Assert.AreEqual("Test Book", currentBook.Title);
            dataProvider.ClearDatabase();
        }
        [TestMethod]
        public void TestAddUser()
        {
            Guid newUserId = Guid.NewGuid();
            dataProvider.AddUser(newUserId, "Test User", "Test Surname");
            var user = dataProvider.GetUserById(newUserId);
            Assert.IsNotNull(user);
            Assert.AreEqual("Test User", user.Name);
            Assert.AreEqual("Test Surname", user.Surname);
            List<IUser> users = dataProvider.GetNUsers(1000, 0);
            Assert.IsNotNull(users);
            IUser user1 = users.FirstOrDefault(u => u.Id == newUserId);
            Assert.AreEqual("Test User", user1.Name);
            dataProvider.ClearDatabase();

        }

        [TestMethod]
        public void TestBorrowAndReturnBook()
        {
            Guid newUserId = Guid.NewGuid();
            Guid newBookId = Guid.NewGuid();
            dataProvider.AddUser(newUserId, "Test User", "Test Surname");
            dataProvider.AddBook(newBookId, "Test Book", "Test Author", false);
            dataProvider.BorrowBook(newUserId, newBookId);
            IBook book = dataProvider.GetBookById(newBookId);
            Assert.IsNotNull(book);
            Assert.IsTrue(book.IsBorrowed);
            dataProvider.ReturnBook(newUserId, newBookId);
            book = dataProvider.GetBookById(newBookId);
            Assert.IsNotNull(book);
            Assert.IsFalse(book.IsBorrowed);
            var returns = dataProvider.GetNReturns(1, 0);
            Console.WriteLine(returns.Count);

            var borrows = dataProvider.GetNBorrows(1, 0);
            Assert.IsNotNull(returns);
            Assert.AreEqual(1, returns.Count);
            Assert.IsNotNull(borrows);
            IReturn return1 = returns.FirstOrDefault(r => r.BookId == newBookId);
            IBorrow borrow1 = borrows.FirstOrDefault(b => b.BookId == newBookId);
            Assert.AreEqual(newUserId, borrow1.UserId);
            Assert.AreEqual(newBookId, borrow1.BookId);
            Assert.AreEqual(newUserId, return1.UserId);
            Assert.AreEqual(newBookId, return1.BookId);
            dataProvider.ClearDatabase();

        }

        [TestMethod]
        public void TestDeleteBook()
        {
            Guid newUserId = Guid.NewGuid();
            Guid newBookId = Guid.NewGuid();
            dataProvider.AddUser(newUserId, "Test User", "Test Surname");
            dataProvider.AddBook(newBookId, "Test Book", "Test Author", false);
            dataProvider.BorrowBook(newUserId, newBookId);
            dataProvider.DeleteBookWithEvents(newBookId);
            var book = dataProvider.GetBookById(newBookId);
            Assert.IsNull(book);
            var borrow = dataProvider.GetNBorrows(1000, 0).FirstOrDefault(b => b.BookId == newBookId);
            Assert.IsNull(borrow);
            var returnE = dataProvider.GetNReturns(1000, 0).FirstOrDefault(r => r.BookId == newBookId);
            Assert.IsNull(returnE);
        }


    }
}
