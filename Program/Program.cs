// LibraryApp/Program.cs
using Library.Logic;
using Library.Data;
using Library.Presentation;
using System;
using Library.Data.Interfaces;
namespace Library.Program
{
    class Program
    {
        static void Main()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\STECU\ONEDRIVE\PULPIT\NOTATKI\SEMESTR 4\PT\REPO\DATALAYER\APP_DATA\LIBRARYDB.MDF;Integrated Security=True;"; // może być z pliku lub zmiennej środowiskowej
            IDataProvider provider = new SqlDataProvider(connectionString);
            var manager = new LibraryService(provider);
            var presentation = new Test(manager);
            presentation.run();
        }
    }
}
