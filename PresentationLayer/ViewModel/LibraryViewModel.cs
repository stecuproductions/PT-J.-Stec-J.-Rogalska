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

        public IBookDto SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public ICommand BorrowBookCommand { get; }
        public ICommand ReturnBookCommand { get; }

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
