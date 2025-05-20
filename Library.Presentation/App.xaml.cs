using System.Configuration;
using System.Data;
using System.Windows;
using Library.Data.API;
using Library.Data.Implementation;
using Library.Logic.API;
using Library.Logic.Implementation;
using Library.Presentation.ViewModel;
using System.Configuration;
using System.Diagnostics.Metrics;
using System;


namespace Library.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILibraryService _libraryService;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=libraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            _libraryService = LibraryServiceFactory.GetLibraryService(connectionString);
            LoginViewModel loginViewModel = new LoginViewModel(_libraryService);
            var loginWindow = new View.LoginWindow(loginViewModel);
            loginWindow.Show();
        }
    }

}
