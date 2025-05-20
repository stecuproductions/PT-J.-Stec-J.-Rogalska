using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library.Logic.API;
using Library.Presentation.Model.Implementation;

namespace Library.Presentation.ViewModel
{
    internal class ReturnViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ILibraryService _libraryService;
        private readonly UserModel _currentUser;
        public ObservableCollection<BookModel> BorrowedBooks { get; set; } = new();
        private BookModel? _selectedBook;
        public BookModel? SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != value)
                {
                    _selectedBook = value;
                    OnPropertyChanged(nameof(SelectedBook));
                    ((RelayCommand)ReturnCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public ICommand ReturnCommand { get; }
        public ReturnViewModel(ILibraryService libraryService, UserModel user)
        {
            _libraryService = libraryService;
            _currentUser = user;
            ReturnCommand = new RelayCommand(ReturnBook, CanReturn);
            LoadBorrowedBooks();
        }
        private void LoadBorrowedBooks()
        {
            BorrowedBooks.Clear();
            IEnumerable<IBorrowLogic> userBorrows = GetAllBorrows();
            IEnumerable<IReturnLogic> userReturns = GetAllReturns();
            List<Guid> addedBooks = new List<Guid>();
            foreach(var borrow in userBorrows)
            {
                if (addedBooks.Contains(borrow.BookId)) continue;
                bool isReturned = userReturns.Any(r =>
                     r.BookId == borrow.BookId &&
                     r.UserId == borrow.UserId &&
                     r.Date > borrow.Date);
                if (!isReturned)
                {
                    IBookLogic book = _libraryService.GetBookByIdLogic(borrow.BookId);
                    if (book != null && book.IsBorrowed)
                    {
                        BorrowedBooks.Add(new BookModel
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Author = book.Author,
                            IsBorrowed = book.IsBorrowed
                        });
                        addedBooks.Add(borrow.BookId);
                    }
                }
            }

        }
        private IEnumerable<IBorrowLogic> GetAllBorrows()
        {
            const int size = 20;
            int offset = 0;
            List<IBorrowLogic> allBorrows = new List<IBorrowLogic>();
            while(true)
            {
                IEnumerable<IBorrowLogic> borrows = _libraryService.GetNBorrowsLogic(size, offset);
                if(borrows == null || !borrows.Any())
                {
                    break;
                }
                foreach(IBorrowLogic bor in borrows)
                {
                    if(bor.UserId ==  _currentUser.Id) allBorrows.Add(bor);
                }
                if(borrows.Count() < size)
                {
                    break;
                }
                offset += size;
            }
            return allBorrows;
        }
        private IEnumerable<IReturnLogic> GetAllReturns()
        {
            const int size = 20;
            int offset = 0;
            List<IReturnLogic> allReturns = new List<IReturnLogic>();
            while (true)
            {
                IEnumerable<IReturnLogic> returns = _libraryService.GetNReturnsLogic(size, offset);
                if (returns == null || !returns.Any())
                {
                    break;
                }
                foreach (IReturnLogic ret in returns)
                {
                    if (ret.UserId == _currentUser.Id) allReturns.Add(ret);
                }
                if (returns.Count() < size)
                {
                    break;
                }
                offset += size;
            }
            return allReturns;
        }
        private bool CanReturn()
        {
            return SelectedBook != null;
        }
        private void ReturnBook()
        {
            if (SelectedBook == null) return;
            bool success = _libraryService.ReturnBookLogic(_currentUser.Id, SelectedBook.Id);
            if (success)
            {
                LoadBorrowedBooks();
                SelectedBook = null;
                MessageBox.Show("Book returned successfully.");
            }
            else
            {
                MessageBox.Show("Failed to return the book.");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
