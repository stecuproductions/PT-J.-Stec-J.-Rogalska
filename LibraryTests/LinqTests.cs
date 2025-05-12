using Library.Data;
using Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.ObjectModel;


namespace LibraryTests.SqlContextTests
{
    [TestClass]
    public class LinqTests
    {
        public TestContext TestContext { get; set; }
        private String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\stecu\\OneDrive\\Pulpit\\Notatki\\Semestr 4\\PT\\Repo\\DataLayer\\App_Data\\LibraryDb.mdf\";Integrated Security=True";
        [TestMethod]
        public void AddingAndDeleteingBookTests()
        {
            IDataProvider provider = new SqlDataProvider(connectionString);
            var newGuid = Guid.NewGuid();
            provider.AddBook(new Book
            {
                Id = newGuid,
                Title = "Test Book",
                Author = "Test Author",
                IsBorrowed = false
            });
            Book newBook = provider.GetBookById(newGuid);
            Assert.IsNotNull(newBook);
            Assert.AreEqual(newBook.Title, "Test Book");
            Assert.AreEqual(newBook.Author, "Test Author");
            Assert.IsFalse(newBook.IsBorrowed);
            provider.DeleteBookWithEvents(newGuid);
            Assert.IsNull(provider.GetBookById(newGuid));
        }
        [TestMethod]
        public void BorrowingAndReturningBookTest()
        {
            IDataProvider provider = new SqlDataProvider(connectionString);
            var newBookGuid = Guid.NewGuid();
            var newUserGuid = Guid.NewGuid();
            var user = new User
            {
                Id = newUserGuid,
                Name = "Test User"
            };
            var newBook = new Book
            {
                Id = newBookGuid,
                Title = "Test Book",
                Author = "Test Author",
                IsBorrowed = false
            };
            provider.AddUser(user);
            provider.AddBook(newBook);

            Book newBookFound = provider.GetBookById(newBookGuid);
            Assert.IsNotNull(newBookFound);
            Assert.AreEqual(newBookFound.Id, newBook.Id);

            var borrowEvt = provider.BorrowBook(user, newBook);
            Assert.IsTrue(provider.GetBookById(newBookGuid).IsBorrowed);
            var returnEvt = provider.ReturnBook(user, newBook);
            Assert.IsFalse(newBook.IsBorrowed);
            Assert.AreEqual(borrowEvt.Description, $"User '{user.Name}' borrowed book '{newBook.Title}'");
            Assert.AreEqual(returnEvt.Description, $"User '{user.Name}' returned book '{newBook.Title}'");

        }
        [TestMethod]
        public void GetAvailableAndUnavailableBooks()
        {
            IDataProvider provider = new SqlDataProvider(connectionString);
            Guid newBookGuid = Guid.NewGuid();
            Guid newCustomerGuid = Guid.NewGuid();
            var newBook = new Book
            {
                Id = newBookGuid,
                Title = "Test Book",
                Author = "Test Author",
                IsBorrowed = false
            };
            provider.AddBook(newBook);
            var newCustomer = new User
            {
                Id = newCustomerGuid,
                Name = "Test User"
            };
            provider.AddUser(newCustomer);
            bool isAvailable = provider.GetAvailableBooks().Any(b => b.Id == newBookGuid);
            Assert.IsTrue(isAvailable);
            provider.BorrowBook(newCustomer, newBook);
            isAvailable = provider.GetAvailableBooks().Any(b => b.Id == newBookGuid);
            Assert.IsFalse(isAvailable);
            provider.ReturnBook(newCustomer, newBook);
            isAvailable = provider.GetAvailableBooks().Any(b => b.Id == newBookGuid);
            Assert.IsTrue(isAvailable);
        }
        [TestMethod]
        public void getAllBooks()
        {
            IDataProvider provider = new SqlDataProvider(connectionString);
            Book newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Author = "Test Author",
                IsBorrowed = false
            };
            provider.AddBook(newBook);
            var allBooks = provider.GetAvailableBooks();
            Assert.IsTrue(allBooks.Count >= 0);
        
    }

}