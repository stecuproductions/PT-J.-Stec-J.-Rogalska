using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.dtos;
using LogicLayer.dtos;

namespace Library.Logic
{
    public interface ILibraryService
    {
        public List<IBookDto> GetBooks();
        public List<IUserDto> GetUsers();
        public List<IEventDto> GetEvents();

        public bool AddBook(string title, string author);
        public bool AddUser(string name);
        public bool RemoveBook(Guid bookId);
        public bool RemoveUser(Guid userId);
        public bool BorrowBook(Guid userId, Guid bookId);
        public bool ReturnBook(Guid userId, Guid bookId);
    }
}
