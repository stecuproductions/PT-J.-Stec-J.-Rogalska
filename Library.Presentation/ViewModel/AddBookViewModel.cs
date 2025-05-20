using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library.Logic.API;

[assembly: InternalsVisibleTo("Library.ViewModelTests")]


namespace Library.Presentation.ViewModel
{
    internal class AddBookViewModel : INotifyPropertyChanged
    {
        private string _title = "";
        private string _author = "";
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                    ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string Author
        {
            get => _author;
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged(nameof(Author));
                    ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public ICommand AddBookCommand { get; }
        public Action? OnBookAdded {  get; set; }
        public Action? CloseAction { get; set; }
        private readonly ILibraryService _libraryService;
        public AddBookViewModel(ILibraryService service)
        {
            _libraryService = service;
            AddBookCommand = new RelayCommand(AddBook, CanAdd);
        }
        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Author);
        }
        private void AddBook()
        {
            bool success = _libraryService.AddBookLogic(Title, Author);
            if (success)
            {
                OnBookAdded?.Invoke();
                CloseAction?.Invoke();
                MessageBox.Show("Book added successfully.");
            }
            else
            {
                MessageBox.Show("Failed to add book.");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
