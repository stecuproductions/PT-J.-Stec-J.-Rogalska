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
using Library.Presentation.Model.Implementation;
using Library.Presentation.View;
using Library.Presentation.ViewModel;

namespace Library.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserModel _user;
        private readonly MainViewModel _viewModel;
        public MainWindow(UserModel user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new MainViewModel(user);
            DataContext = _viewModel;
            _viewModel.RequestOpenWindow += ViewModel_RequestOpenWindow;
            Title = $"Welcome, {_user.Name} {_user.Surname}";
        }
        private void ViewModel_RequestOpenWindow(UserModel user, string windowType)
        {
            if(windowType == "Borrow")
            {
                new BorrowWindow(user).Show();
            }
            else if(windowType == "Return")
            {
                new ReturnWindow(user).Show();
            }
        }
    }
}