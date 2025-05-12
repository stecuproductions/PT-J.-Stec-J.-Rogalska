using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Library.Data;
using Library.Logic.dtos;
[assembly: InternalsVisibleTo("LibraryTests")]
namespace Library.Logic
{

    internal class LibraryService : ILibraryService
    {
        private readonly IDataProvider _dataProvider;

        public LibraryService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


        public List<EventDto> GetEvents()
        {
            return _dataProvider.GetEvents()
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Timestamp = e.Timestamp,
                    Description = e.Description,
                    BookId = e.BookId,
                    UserId = e.UserId
                })
                .ToList();
        }

        public bool AddUser(string name)
        {
            var state = _dataProvider.GetLibraryState();
            if (state.Users.Any(u => u.Name == name)) return false;
            Guid newId = Guid.NewGuid();

            _dataProvider.AddUser(name, newId);
            return true;
        }

        public bool AddBook(string title, string author)
        {
            var state = _dataProvider.GetLibraryState();
            if (state.Books.Any(b => b.Title == title && b.Author == author)) return false;
            Guid newId = Guid.NewGuid();
            _dataProvider.AddBook(title, author, newId);
            return true;
        }

        public bool RemoveUser(Guid userId)
        {
            var state = _dataProvider.GetLibraryState();
            var user = state.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;
            _dataProvider.RemoveUser(user);
            return true;
        }
        public bool RemoveBook(Guid bookId)
        {
            var state = _dataProvider.GetLibraryState();
            var book = state.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;
            _dataProvider.RemoveBook(book);
            return true;
        }


        public bool BorrowBook(Guid userId, Guid bookId)
        {
            var state = _dataProvider.GetLibraryState();
            var user = state.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            var book = state.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null || book.IsBorrowed) return false;

            _dataProvider.BorrowBook(user, book);
            return true;
        }

        public bool ReturnBook(Guid userId, Guid bookId)
        {
            var state = _dataProvider.GetLibraryState();
            var user = state.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            var book = state.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null || !book.IsBorrowed) return false;

            _dataProvider.ReturnBook(user, book);
            return true;
        }




        public List<UserDto> GetUsers()
        {
            return _dataProvider.GetLibraryState()
                .Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name
                })
                .ToList();
        }

        public List<BookDto> GetBooks()
        {
            return _dataProvider.GetLibraryState()
                .Books
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IsBorrowed = b.IsBorrowed
                })
                .ToList();
        }

    }
}