using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LibraryTests")]

namespace Library.Data
{
    internal class SqlDataProvider : IDataProvider
    {
        private readonly string _connectionString;

        public SqlDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Event AddBook(Book book)
        {
            using var db = new LibraryDbDataContext(_connectionString);
            db.Book.InsertOnSubmit(book);
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"Book '{book.Title}' was added",
                BookId = book.Id
            };
            db.Event.InsertOnSubmit(newEvent);
            db.SubmitChanges();
            return newEvent;

        }


        public Event BorrowBook(User user, Book book)
        {
            using var db = new LibraryDbDataContext(_connectionString);
            var dbBook = db.Book.FirstOrDefault(b => b.Id == book.Id);

            if (dbBook is null || dbBook.IsBorrowed)
                throw new Exception("Book is already borrowed or does not exist.");

            dbBook.IsBorrowed = true;           

            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"User '{user.Name}' borrowed book '{book.Title}'",
                UserId = user.Id,
                BookId = book.Id
            };

            db.Event.InsertOnSubmit(newEvent);  
            db.SubmitChanges();                 

            return newEvent;
        }



        public Event DeleteBookWithEvents(Guid id)
        {
            using var db = new LibraryDbDataContext(_connectionString);

            var book = db.Book.FirstOrDefault(b => b.Id == id);
            if (book is null)
                throw new Exception("Book is already deleted or does not exist.");

            var relatedEvents = db.Event.Where(e => e.BookId == id);
            db.Event.DeleteAllOnSubmit(relatedEvents);
            db.SubmitChanges();

            db.Book.DeleteOnSubmit(book);

            var deletionEvent = new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"Book '{book.Title}' and its events were deleted"
            };
            db.Event.InsertOnSubmit(deletionEvent);

            db.SubmitChanges();
            return deletionEvent;
        }


        public List<Book> GetAvailableBooks()
        {
            using var db = new LibraryDbDataContext(_connectionString);
            var query = from book in db.Book
                        where !book.IsBorrowed
                        select book;
            return query.ToList();
        }

        public List<Book> GetBorrowedBooks()
        {
            using var db = new LibraryDbDataContext(_connectionString);
            var query = from book in db.Book
                        where book.IsBorrowed
                        select book;
            return query.ToList();
        }

        public Book? GetBookById(Guid id)
        {
            using var db = new LibraryDbDataContext(_connectionString);
            return db.Book.FirstOrDefault(b => b.Id == id);
        }

        public User? GetUserById(Guid id)
        {
            using var db = new LibraryDbDataContext(_connectionString);
            return db.User.FirstOrDefault(u => u.Id == id);
        }


        public Event? ReturnBook(User user, Book book)
        {
            using var db = new LibraryDbDataContext(_connectionString);
            var dbBook = db.Book.FirstOrDefault(b => b.Id == book.Id);
            if (dbBook is not null && dbBook.IsBorrowed)
            {
                dbBook.IsBorrowed = false;

                var newEvent = new Event
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    Description = $"User '{user.Name}' returned book '{book.Title}'",
                    UserId = user.Id,
                    BookId = book.Id
                };

                db.Event.InsertOnSubmit(newEvent);
                db.SubmitChanges();
                return newEvent;
            }
            throw new Exception("Book is already returned or does not exist.");
        }

        public Event? AddUser(User user)
        {
            using var db = new LibraryDbDataContext(_connectionString);
            db.User.InsertOnSubmit(user);
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = $"User '{user.Name}' was added",
                UserId = user.Id
            };
            db.Event.InsertOnSubmit(newEvent);
            db.SubmitChanges();
            return newEvent;
        }

        public List<Book> GetBooks()
        {
            using var db = new LibraryDbDataContext(_connectionString);
            return db.Book.ToList();
        }

    }
}
