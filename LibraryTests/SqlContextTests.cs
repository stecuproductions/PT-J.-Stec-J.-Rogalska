using Library.Data; 


namespace LibraryTests.SqlContextTests
{
    [TestClass]
    public class SqlContextTests
    {
        public TestContext TestContext { get; set; }
        private String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\stecu\\OneDrive\\Pulpit\\Notatki\\Semestr 4\\PT\\Repo\\DataLayer\\App_Data\\LibraryDb.mdf\";Integrated Security=True"; //Zastap to wlasciwym connection stringiem
        [TestMethod]
        public void SqlContextWorks()
        {
            var context = new LibraryDbDataContext(connectionString);
            TestContext.WriteLine("Context created");
            var users = context.Users.ToList();
            TestContext.WriteLine("Users fetched");
            foreach (var user in users)
            {
                TestContext.WriteLine($"User: {user.Id}, {user.Name}");
            }
            Assert.IsNotNull(users);
        }
        [TestMethod]
        public void InsertingAndDeletingWorks()
        {
           throw new NotImplementedException();
        }
    }

}