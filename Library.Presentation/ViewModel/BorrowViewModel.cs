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
using Library.Presentation.View;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library.Presentation.ViewModel
{
    public class BorrowViewModel : INotifyPropertyChanged
    {
        private readonly ILibraryService _libraryService;
        private readonly UserModel _currentUser;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<BookModel> AvailableBooks { get; set; } = new();
        private BookModel? _selectedBook;
        public BookModel? SelectedBook
        {
            get => _selectedBook;
            set
            {
                if(_selectedBook != value)
                {
                    _selectedBook = value;
                    OnPropertyChanged(nameof(SelectedBook));
                    ((RelayCommand)BorrowCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteBookCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public ICommand BorrowCommand { get; }
        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public BorrowViewModel(ILibraryService libraryService, UserModel user)
        {
            _libraryService = libraryService;
            _currentUser = user;
            BorrowCommand = new RelayCommand(BorrowBook, CanBorrow);
            AddBookCommand = new RelayCommand(OpenAddBookWindow);
            DeleteBookCommand = new RelayCommand(DeleteBook, CanDelete);
            LoadAvailableBooks();
        }
        private void OpenAddBookWindow()
        {
            new AddBookWindow(_libraryService, LoadAvailableBooks).ShowDialog();
        }
        private void LoadAvailableBooks()
        {
            AvailableBooks.Clear();
            IEnumerable<IBookLogic> books = _libraryService.GetNBooksLogic(100, 0);
            if (books == null) return;
            foreach(IBookLogic book in books)
            {
                if(!book.IsBorrowed)
                {
                    AvailableBooks.Add(new BookModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        IsBorrowed = book.IsBorrowed
                    });
                }
            }
        }
        private bool CanBorrow()
        {
            return SelectedBook != null;
        }
        private void BorrowBook()
        {
            if(SelectedBook == null) return;
            bool success = _libraryService.BorrowBookLogic(_currentUser.Id, SelectedBook.Id);
            if(success)
            {
                LoadAvailableBooks();
                SelectedBook = null;
                MessageBox.Show("Book borrowed successfully.");
            }
            else
            {
                MessageBox.Show("Failed to borrow the book");
            }
        }
        private void DeleteBook()
        {
            if(SelectedBook == null) return;
            MessageBoxResult result = MessageBox.Show(
            $"Are you sure you want to delete '{SelectedBook.Title}'?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool success = _libraryService.DeleteBookWithEventsLogic(SelectedBook.Id);
                if(success)
                {
                    AvailableBooks.Remove(SelectedBook);
                    SelectedBook = null;
                    MessageBox.Show("Book deleted successfullt.");
                }
                else
                {
                    MessageBox.Show("Failed to delete book.");
                }
            }
        }
        private bool CanDelete()
        {
            return SelectedBook != null;
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
