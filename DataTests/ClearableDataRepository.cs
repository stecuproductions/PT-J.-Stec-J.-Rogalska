using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Implementation;

namespace Library.DataTests
{
    internal class ClearableDataRepository : DataRepository
    {

        
        public ClearableDataRepository(string connectionString) : base(connectionString)
        {
        }

        public void ClearDatabase()
        {
            _context.book.DeleteAllOnSubmit(_context.book);
            _context.user.DeleteAllOnSubmit(_context.user);
            _context.borrow.DeleteAllOnSubmit(_context.borrow);
            _context.returnE.DeleteAllOnSubmit(_context.returnE);
            _context.SubmitChanges();
        }
    }
}
