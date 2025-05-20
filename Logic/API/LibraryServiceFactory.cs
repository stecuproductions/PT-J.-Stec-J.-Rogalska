using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.Implementation;
using Library.Data.API;
namespace Library.Logic.API
{
    public static class LibraryServiceFactory
    {
        public static ILibraryService GetLibraryService()
        {
            return new LibraryService(DataRepositoryFactory.GetDataRepository());
        }
        public static ILibraryService GetLibraryService(string connectionString)
        {
            return new LibraryService(DataRepositoryFactory.GetDataRepository(connectionString));
        }
    }
}
