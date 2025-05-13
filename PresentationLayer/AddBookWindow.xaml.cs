using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Library.Logic;
using PresentationLayer.ViewModel;

namespace PresentationLayer
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private readonly ILibraryService _libraryService;
        public AddBookWindow()
        {
            InitializeComponent();
            var vm = (Application.Current.MainWindow as MainWindow)?.DataContext as LibraryViewModel;
            _libraryService = vm?.LibraryService;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
            {
                bool success = _libraryService.AddBook(title, author);

                if (success)
                {
                    MessageBox.Show("Book added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This book already exists in the library.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Title and Author must not be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
