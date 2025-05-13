using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library.Logic;
using LogicLayer;
using PresentationLayer.ViewModel;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ILibraryService libraryService = LibraryServiceFactory.CreateLibraryService();
            DataContext = new LibraryViewModel(libraryService);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(booksList.SelectedItem != null)
            { 
                RemoveBookButton.Visibility = Visibility.Visible;
                BorrowReturnButton.Visibility = Visibility.Visible;
            }
            else
            {
                RemoveBookButton.Visibility= Visibility.Collapsed;
                BorrowReturnButton.Visibility= Visibility.Collapsed;
            }
        }

        private void BorrowReturnButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as LibraryViewModel;
            if(vm?.SelectedBookTitle != null)
            {
                if(vm.SelectedBook.IsBorrowed)
                {
                    vm.ReturnBookCommand.Execute(null);
                }
                else
                {
                    vm.BorrowBookCommand.Execute(null);
                }
            }
        }
    }
}