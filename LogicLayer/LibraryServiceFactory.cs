using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Library.Data;
using Library.Logic;

namespace LogicLayer
{
    public static class LibraryServiceFactory
    {
        public static ILibraryService CreateLibraryService()
        {
            IDataProvider provider = DataProviderFactory.CreateSqlDataProvider();
            return new LibraryService(provider);
        }
    }
}
