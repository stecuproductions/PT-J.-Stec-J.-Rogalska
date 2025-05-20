using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library.Logic.API;
using Library.Presentation.Model.API;
using Library.Presentation.Model.Implementation;

namespace Library.Presentation.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ILibraryService _libraryService;
        private string _name;
        public string name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }
        private string _surname;
        public string surname
        {
            get => _surname;
            set
            {
                if(_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(surname));
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public ICommand LoginCommand { get; }
        public LoginViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            LoginCommand = new RelayCommand(Login, CanLogin);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname);
        }
        private void Login()
        {
            UserModel? user = FindUserByName(name, surname);
            if (user == null)
            {
                bool created = _libraryService.AddUserLogic(name, surname);
                if (!created) 
                {
                    MessageBox.Show("User creation failed.");
                    return;
                }
                MessageBox.Show("New user created.");
                user = FindUserByName(name, surname);
            }
            MainWindow main = new MainWindow(user, _libraryService);
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = main;
            main.Show();
        }
        private UserModel? FindUserByName(string name, string surname)
        {
            const int pageSize = 20;
            int offset = 0;
            while(true)
            {
                IEnumerable<IUserLogic> users = _libraryService.GetNUsersLogic(pageSize, offset);
                if (users == null || !users.Any())
                {
                    return null;
                }
                foreach (IUserLogic user in users)
                {
                    if(user.Name == name && user.Surname == surname)
                    {
                        return ConvertFromLogic(user);
                    }
                }
                offset+= pageSize;
            }
        }

        private UserModel ConvertFromLogic(IUserLogic userLogic)
        {
            return new UserModel
            {
                Id = userLogic.Id,
                Name = userLogic.Name,
                Surname = userLogic.Surname
            };
        }
    }
}
