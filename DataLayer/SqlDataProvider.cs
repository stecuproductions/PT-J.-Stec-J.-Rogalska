using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Program")]

namespace Library.Data.Interfaces
{
    //sqllocaldb stop MSSQLLocalDB - to tak dal przypomnienia
    internal class SqlDataProvider : IDataProvider
    {
        //ten connection string nalezy zmienic i dostosowac do swojego
        private String connectionString;
        
        public SqlDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEvent AddEvent(string description, IUser? user = null, IBook? book = null)
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            Event newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Description = description,
                BookId = book?.Id,
                UserId = user?.Id
            };
            db.DbEvent.InsertOnSubmit(SqlMapper.ModelEventToDbEvent(newEvent));
            db.SubmitChanges();
            return newEvent;
        }


        public List<IEvent> GetEvents()
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbEvents = db.DbEvent.ToList();
            List<IEvent> events = new List<IEvent>();
            foreach (var dbEvent in dbEvents)
            {
                events.Add(SqlMapper.DbEventToModelEvent(dbEvent));
            }
            return events;
        }

        public ILibraryState GetLibraryState()
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbBooks = db.DbBook.ToList();
            var dbUsers = db.DbUser.ToList();
            var modelBooks = new List<IBook>();
            foreach (var dbBook in dbBooks)
            {
                modelBooks.Add(SqlMapper.DbBookToModelBook(dbBook));
            }
            var modelUsers = new List<IUser>();
            foreach (var dbUser in dbUsers)
            {
                modelUsers.Add(SqlMapper.DbUserToModelUser(dbUser));
            }
            return new LibraryState
            {
                Books = modelBooks,
                Users = modelUsers
            };
        }

        public IEvent AddUser(string name, Guid id)
        {
            IUser user = new User
            {
                Id = id,
                Name = name
            };
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbUser = SqlMapper.ModelUserToDbUser(user);
            db.DbUser.InsertOnSubmit(dbUser);
            db.SubmitChanges();
            return AddEvent($"User {user.Name} {user.Id} added", user, null);
        }

        public IEvent BorrowBook(IUser user, IBook book)
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbBook = db.DbBook.FirstOrDefault(b => b.Id == book.Id);
            dbBook.IsBorrowed = true;
            db.SubmitChanges();
            return AddEvent($"User {user.Name} {user.Id} borrowed book {book.Title} {book.Id}", user, book);
        }
        public IEvent AddBook(string title, String author, Guid id)
        {
            IBook book = new Book
            {
                Id = id,
                Title = title,
                Author = author,
                IsBorrowed = false
            };
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbBook = SqlMapper.ModelBookToDbBook(book);
            db.DbBook.InsertOnSubmit(dbBook);
            db.SubmitChanges();
            return AddEvent($"Book {book.Title} {book.Id} added", null, book);

        }


        public IEvent RemoveBook(IBook book)
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbBook = db.DbBook.FirstOrDefault(b => b.Id == book.Id);
            if (dbBook == null)
                throw new Exception("Book not found");
            var removalEvent = AddEvent($"Book {book.Title} {book.Id} and related events removed", null, null);
            var relatedEvents = db.DbEvent.Where(e => e.BookId == book.Id).ToList();
            foreach (var dbEvent in relatedEvents)
            {
                db.DbEvent.DeleteOnSubmit(dbEvent);
            }

            // Usuń książkę
            db.DbBook.DeleteOnSubmit(dbBook);
            db.SubmitChanges();

            return removalEvent;
        }



        public IEvent RemoveUser(IUser user)
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbUser = db.DbUser.FirstOrDefault(u => u.Id == user.Id);
            List<DbEvent> dbEvents = db.DbEvent.Where(e => e.DbUser.Id == user.Id).ToList();
            foreach (var dbEvent in dbEvents)
            {
                db.DbEvent.DeleteOnSubmit(dbEvent);
            }
            db.SubmitChanges();
            db.DbUser.DeleteOnSubmit(dbUser);
            db.SubmitChanges();
            return AddEvent($"User {user.Name} {user.Id} and its children removed", null, null);
        }

        public IEvent ReturnBook(IUser user, IBook book)
        {
            using LibraryDbDataContext db = new LibraryDbDataContext(connectionString);
            var dbBook = db.DbBook.FirstOrDefault(b => b.Id == book.Id);
            dbBook.IsBorrowed = false;
            db.SubmitChanges();
            return AddEvent($"User {user.Name} {user.Id} returned book {book.Title} {book.Id}", user, book);
        }
    }
}
