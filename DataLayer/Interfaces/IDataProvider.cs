using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IDataProvider
    {
        ILibraryState GetLibraryState();
        List<IEvent> GetEvents();
        IEvent AddBook(string title, string author, Guid id);
        IEvent AddUser(string name, Guid id);
        IEvent RemoveBook(IBook book);
        IEvent RemoveUser(IUser user);
        IEvent BorrowBook(IUser user, IBook book);
        IEvent ReturnBook(IUser user, IBook book);

        IEvent AddEvent(string descriptionm, IUser? user = null , IBook? book = null);

    }
}
