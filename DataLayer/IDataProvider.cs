using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IDataProvider
    {
        User? GetUserById(Guid id);
        Book? GetBookById(Guid id);
        List<Book> GetAvailableBooks();
        Event? DeleteBookWithEvents(Guid id);
        Event? AddBook(Book book);
        Event BorrowBook(User user, Book book);
        Event? ReturnBook(User user, Book book);
        public Event? AddUser(User user);
    }
}
