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
using Library.Presentation.Model.Implementation;
using Library.Presentation.ViewModel;

namespace Library.Presentation.View
{
    /// <summary>
    /// Logika interakcji dla klasy BorrowWindow.xaml
    /// </summary>
    public partial class BorrowWindow : Window
    {
        private readonly UserModel _user;
        private readonly BorrowViewModel _borrowViewModel;
        public BorrowWindow(UserModel user, ILibraryService libraryService)
        {
            InitializeComponent();
            _user = user;
            _borrowViewModel = new BorrowViewModel(libraryService, user);
            DataContext = new BorrowViewModel(libraryService, user);
            Title = $"Borrow Books - {_user.Name} {_user.Surname}";
        }
    }
}
