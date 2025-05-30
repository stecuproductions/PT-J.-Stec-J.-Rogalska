﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.Presentation.Model.Implementation;

[assembly: InternalsVisibleTo("Library.ViewModelTests")]


namespace Library.Presentation.ViewModel
{
    internal class MainViewModel
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
