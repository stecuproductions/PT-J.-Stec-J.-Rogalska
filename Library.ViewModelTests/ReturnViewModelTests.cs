using Library.Logic.API;
using Library.Presentation.Model.Implementation;
using Library.Presentation.ViewModel;
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
            UserModel user = new UserModel { Id = _testUserId};
            ReturnViewModel vm = new ReturnViewModel(_mockService, user);
            Assert.AreEqual(2, vm.BorrowedBooks.Count);
            Assert.IsTrue(vm.BorrowedBooks.All(b => b.IsBorrowed));
        }
        [TestMethod]
        public void ReturnBookCommand()
        {
            UserModel user = new UserModel { Id= _testUserId};
            ReturnViewModel vm = new ReturnViewModel(_mockService, user);
            vm.SelectedBook = vm.BorrowedBooks.First();
            Assert.IsTrue(vm.ReturnCommand.CanExecute(null));
            Guid selectedBookId = vm.SelectedBook?.Id ?? Guid.Empty;
            vm.ReturnCommand.Execute(null);
            Assert.IsFalse(vm.BorrowedBooks.Any(b => b.Id == selectedBookId));
            IBookLogic returnedBook = _mockService.GetBookByIdLogic(selectedBookId);
            Assert.IsNotNull(returnedBook);
            Assert.IsFalse(returnedBook.IsBorrowed);
        }
        [TestMethod]
        public void ReturnCommand_WithoutSelectedBook()
        {
            UserModel user = new UserModel { Id = _testUserId };
            ReturnViewModel vm = new ReturnViewModel(_mockService, user);
            vm.SelectedBook = null;
            Assert.IsFalse(vm.ReturnCommand.CanExecute(null));
        }
    }
}
