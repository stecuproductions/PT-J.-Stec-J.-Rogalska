using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Implementation;
namespace Library.Data.API
{
    public abstract class IDataRepository
    {
        //Users
        public abstract void AddUser(Guid id, string name, string surname);
        public abstract IUser GetUserById(Guid id);
        public abstract List<IUser> GetNUsers(int n, int offset);
        //Books
        public abstract void AddBook(Guid id, string title, string author, bool isBorrowed);
        public abstract IBook GetBookById(Guid id);
        public abstract List<IBook> GetNBooks(int n, int offset);
        //Events
        public abstract void BorrowBook(Guid userId, Guid bookId);
        public abstract void ReturnBook(Guid userId, Guid bookId);
        public abstract List<IReturn> GetNReturns(int n, int offset);
        public abstract List<IBorrow> GetNBorrows(int n, int offset);

        public abstract void DeleteBookWithEvents(Guid id);
        //Constructors

    }
}
