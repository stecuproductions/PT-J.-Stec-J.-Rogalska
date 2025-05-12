using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.dtos;

namespace Library.Logic
{
    public interface ILibraryService
    {
        List<BookDto> GetBooks();
        List<UserDto> GetUsers();
        List<EventDto> GetEvents();

        bool AddBook(string title, string author);
        bool AddUser(string name);
        bool RemoveBook(Guid bookId);
        bool RemoveUser(Guid userId);
        bool BorrowBook(Guid userId, Guid bookId);
        bool ReturnBook(Guid userId, Guid bookId);
    }
}
