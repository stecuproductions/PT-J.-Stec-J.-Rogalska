using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Interfaces;

namespace Library.Data
{
    public static class DataProviderFactory
    {
        public static IDataProvider CreateSqlDataProvider()
        {
            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jroga\\Desktop\\studia\\IV\\PT\\progtech\\PT-J.-Stec-J.-Rogalska\\DataLayer\\App_Data\\LibraryDb.mdf;Integrated Security=True";
            return new SqlDataProvider(connString);
        }
    }
}
