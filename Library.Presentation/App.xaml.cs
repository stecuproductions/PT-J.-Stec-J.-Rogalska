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
            //string connectionString = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
            IDataRepository dataRepository = new DataRepository("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jroga\\Desktop\\studia\\IV\\PT\\progtech\\task2\\App_Data\\LibraryDb.mdf;Integrated Security=True");
            _libraryService = new LibraryService(dataRepository);
            LoginViewModel loginViewModel = new LoginViewModel(_libraryService);
            var loginWindow = new View.LoginWindow(loginViewModel);
            loginWindow.Show();
        }
    }

}
