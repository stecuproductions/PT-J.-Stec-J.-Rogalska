using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.Presentation.Model.Implementation;

namespace Library.Presentation.ViewModel
{
    public class MainViewModel
    {
        public UserModel User { get; }
        public ICommand OpenBorrowCommand { get; }
        public ICommand OpenReturnCommand { get; }
        public event Action<UserModel, string>? RequestOpenWindow;
        public MainViewModel(UserModel user)
        {
            User = user;
            OpenBorrowCommand = new RelayCommand(OpenBorrow);
            OpenReturnCommand = new RelayCommand(OpenReturn);
        }
        private void OpenBorrow()
        {
            RequestOpenWindow?.Invoke(User, "Borrow");
        }
        private void OpenReturn()
        {
            RequestOpenWindow?.Invoke(User, "Return");
        }
    }
}
