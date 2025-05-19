using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;
using Library.Logic.Implementation;
namespace Library.Logic.API
{
    public abstract class ILibraryService
    {
        private IDataRepository _dataRepository;
        public abstract IEnumerable<IBookLogic> GetNBooksLogic(int n, int offset);
        public abstract IBookLogic GetBookByIdLogic(Guid id);
        public abstract bool AddBookLogic(string title, string author);
        public abstract bool AddUserLogic(string name, string surname);
        public abstract IEnumerable<IUserLogic> GetNUsersLogic(int n, int offset);
        public abstract bool BorrowBookLogic(Guid userId, Guid bookId);
        public abstract bool ReturnBookLogic(Guid userId, Guid bookId);
        public abstract bool DeleteBookWithEventsLogic(Guid bookId);
        public abstract IEnumerable<IBorrowLogic> GetNBorrowsLogic(int n, int offset);
        public abstract IEnumerable<IReturnLogic> GetNReturnsLogic(int n, int offset);

        //factory method
        ILibraryService(string connectionString)
        {
            _dataRepository = IDataRepository.GetDataRepository(connectionString);
        }
        public ILibraryService()
        {
            _dataRepository = IDataRepository.GetDataRepository();
        }
    }
}
