using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library.Logic;
using LogicLayer.dtos;

namespace PresentationLayer.ViewModel
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ILibraryService _libraryService;

        private readonly Guid currentUserId;

        public ObservableCollection<IBookDto> Books { get; set; }

        private IBookDto _selectedBook;

        public ILibraryService LibraryService => _libraryService;

        public IBookDto SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(BorrowReturnCommand));
                OnPropertyChanged(nameof(BorrowReturnText));
                UpdateBookDetails();
            }
        }

        public string SelectedBookTitle { get; set; }
        public string SelectedBookAuthor { get; set; }
        public string SelectedBookAvailability {  get; set; }

        public ICommand BorrowBookCommand { get; }
        public ICommand ReturnBookCommand { get; }
        public ICommand AddBookCommand { get; }
        public ICommand RemoveBookCommand { get; }
        public ICommand BorrowReturnCommand => SelectedBook.IsBorrowed ? ReturnBookCommand : BorrowBookCommand;

        public string BorrowReturnText => SelectedBook != null && SelectedBook.IsBorrowed ? "Return Book" : "Borrow Book";

        public LibraryViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            var users = _libraryService.GetUsers();
            if (users != null && users.Any())
            {
                currentUserId = users.First().Id;
            }
            else
            {
                currentUserId = Guid.Empty; 
            }
            Books = new ObservableCollection<IBookDto>(_libraryService.GetBooks());
            BorrowBookCommand = new RelayCommand(BorrowBook, CanBorrow);
            ReturnBookCommand = new RelayCommand(ReturnBook, CanReturn);
            AddBookCommand = new RelayCommand(OpenAddBookDialog);
            RemoveBookCommand = new RelayCommand(RemoveBook, CanRemove);
        }

        private void RemoveBook()
        {
            if(SelectedBook != null)
            {
                var confirmWindow = new ConfirmDelete(SelectedBook.Title, SelectedBook.Author)
                {
                    Owner = Application.Current.MainWindow
                };
                confirmWindow.ShowDialog();
                if(confirmWindow.confirmed)
                {
                    _libraryService.RemoveBook(SelectedBook.Id);
                    Books.Remove(SelectedBook);
                    SelectedBook = null;
                }
            }
        }

        private bool CanRemove()
        {
            return SelectedBook != null;
        }

        private void OpenAddBookDialog()
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.ShowDialog();
            Books.Clear();
            var updated = _libraryService.GetBooks();
            foreach (var book in updated)
            {
                Books.Add(book);
            }
        }

        private void UpdateBookDetails()
        {
            if(SelectedBook != null)
            {
                SelectedBookTitle = SelectedBook.Title;
                SelectedBookAuthor = SelectedBook.Author;
                SelectedBookAvailability = SelectedBook.IsBorrowed ? "Borrowed" : "Available";
                OnPropertyChanged (nameof(SelectedBookTitle));
                OnPropertyChanged (nameof(SelectedBookAuthor));
                OnPropertyChanged (nameof(SelectedBookAvailability));
                OnPropertyChanged(nameof(BorrowReturnText));
            }
        }

        private void UpdateBookInCollection()
        {
            var updatedBook = _libraryService.GetBooks().FirstOrDefault(b => b.Id == SelectedBook.Id);
            if (updatedBook != null)
            {
                var index = Books.IndexOf(SelectedBook);
                if(index >= 0)
                {
                    Books[index] = updatedBook;
                }
            }
        }

        private void BorrowBook()
        {
            _libraryService.BorrowBook(currentUserId, SelectedBook.Id);
            UpdateBookDetails();
            UpdateBookInCollection();
        }

        private bool CanBorrow()
        {
            return !SelectedBook.IsBorrowed;
        }

        private bool CanReturn()
        {
            return SelectedBook.IsBorrowed;
        }

        private void ReturnBook()
        {
            _libraryService.ReturnBook(currentUserId, SelectedBook.Id);
            UpdateBookDetails();
            UpdateBookInCollection();
        }

        public bool IsSelected
        {
            get { return SelectedBook != null; }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
