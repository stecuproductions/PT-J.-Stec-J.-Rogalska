using Library.Logic.API;
using Library.ViewModelTests.Mocks;

namespace Library.ReturnViewModelTests
{
    [TestClass]
    public class ReturnViewModelTests
    {
        private Guid _testUserId = Guid.NewGuid();
        private MockLibraryService _mockService;
        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockLibraryService();
            _mockService.AddBookLogic("Test 1", "Author A");
            _mockService.AddBookLogic("Test 2", "Author B");
            IBookLogic book1 = _mockService.GetNBooksLogic(1, 0).First();
            IBookLogic book2 = _mockService.GetNBooksLogic(1, 1).First();
            _mockService.BorrowBookLogic(_testUserId, book1.Id);
            _mockService.BorrowBookLogic(_testUserId, book2.Id);
        }
        [TestMethod]
        public void LoadBorrowedBooks()
        {
            MockUser user = new MockUser { Id = _testUserId};

        }
    }
}
