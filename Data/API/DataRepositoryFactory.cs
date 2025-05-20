using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Implementation;

namespace Library.Data.API
{
    public static class DataRepositoryFactory
    {
        public static IDataRepository GetDataRepository()
        {
            return new DataRepository();
        }
        public static IDataRepository GetDataRepository(string connectionString)
        {
            return new DataRepository(connectionString);
        }
    }
}
