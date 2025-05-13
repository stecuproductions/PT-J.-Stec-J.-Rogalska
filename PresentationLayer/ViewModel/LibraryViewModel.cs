using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                UpdateBookDetails();
            }
        }

        public string SelectedBookTitle { get; set; }
        public string SelectedBookAuthor { get; set; }
        public string SelectedBookAvailability {  get; set; }

        public ICommand BorrowBookCommand { get; }
        public ICommand ReturnBookCommand { get; }
        public ICommand AddBookCommand { get; }

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
            }
        }

        private void BorrowBook()
        {
            _libraryService.BorrowBook(currentUserId, SelectedBook.Id);
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
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
