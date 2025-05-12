using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LibraryTests")]

namespace Library.Data
{
    internal class InMemoryDataProvider : IDataProvider
    {
        protected readonly ILibraryState _state;
        private readonly List<IEvent> _events = new();

        public InMemoryDataProvider()
        {
            _state = new LibraryState 
            {
                Users = new List<IUser>(),
                Books = new List<IBook>()
            };
        }

        public ILibraryState GetLibraryState()
        {
            return _state;
        }
        public List<IEvent> GetEvents()
        {
            return _events;
        }
        public void GenerateEmptyDataSet()
        {
            _state.Books.Clear();
            _state.Users.Clear();
            _events.Clear();
        }

        public IEvent AddEvent(string description, IUser? user=null, IBook?book= null)
        {
            Event newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = description,
                UserId = user.Id,
                BookId = book.Id,
            };
            _events.Add(newEvent);
            return newEvent;
        }

        public IEvent AddBook(string title, string author, Guid id)
        {
            IBook book = new Book
            {
                Id = id,
                Title = title,
                IsBorrowed = false
            };
            _state.Books.Add(book);
            return new Event {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"Book '{book.Title}'  '{book.Id}' added.",
                BookId = book.Id
            };

        }

        public IEvent AddUser(string name, Guid id)
        {
            IUser user = new User
            {
                Id = id,
                Name = name
            };
            _state.Users.Add(user);
            return new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"User '{user.Name}' '{user.Id}' added.",
                UserId = user.Id
            };
        }

        public IEvent RemoveBook(IBook book)
        {
            _state.Books.Remove(book);
            return new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"Book '{book.Title}' '{book.Id}' removed.",
                BookId = book.Id
            };
        }

        public IEvent RemoveUser(IUser user)
        {
           _state.Users.Remove(user);
            return new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"User '{user.Name}' '{user.Id}' removed.",
                UserId = user.Id
            };
        }

        public IEvent BorrowBook(IUser user, IBook book)
        {
            _state.Books.FirstOrDefault(b => b.Id == book.Id).IsBorrowed = true;
            return new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"User '{user.Name}' '{user.Id}' borrowed '{book.Title}'",
                UserId = user.Id,
                BookId = book.Id
            };

        }

        public IEvent ReturnBook(IUser user, IBook book)
        {
            _state.Books.FirstOrDefault(b => b.Id == book.Id).IsBorrowed = false;
            return new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"User '{user.Name}' '{user.Id}' returned '{book.Title}'",
                UserId = user.Id,
                BookId = book.Id
            };
        }
    }
}
