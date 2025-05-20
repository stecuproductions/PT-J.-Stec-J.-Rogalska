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
using Library.Logic.API;
using Library.Presentation.ViewModel;

namespace Library.Presentation.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow(ILibraryService service, Action onBookAdded)
        {
            InitializeComponent();
            AddBookViewModel viewModel = new AddBookViewModel(service);
            viewModel.OnBookAdded = onBookAdded;
            viewModel.CloseAction = Close;
            DataContext = viewModel;
        }
    }
}
