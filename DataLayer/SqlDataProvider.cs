using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    internal class SqlDataProvider : IDataProvider
    {
        private readonly LibraryDbDataContext _context;
        

        public SqlDataProvider(LibraryDbDataContext context)
        {
            _context = context;
        }


        public void AddEvent(IUser user, IBook book, string description)
        {
            throw new NotImplementedException();
        }

        public void GenerateEmptyDataSet()
        {
            throw new NotImplementedException();
        }

        public List<IEvent> GetEvents()
        {
            throw new NotImplementedException();
        }

        public ILibraryState GetLibraryState()
        {
            throw new NotImplementedException();
        }
    }
}
