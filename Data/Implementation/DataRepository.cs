using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;
//SqlLocalDB stop MSSQLLocalDB

[assembly: InternalsVisibleTo("Library.DataTests")]
[assembly: InternalsVisibleTo("Library.LogicTests")]

namespace Library.Data.Implementation
{
    internal class DataRepository : IDataRepository
    {
        protected LibraryDataContext _context;

        public DataRepository(string connectionString)
        {
            _context = new LibraryDataContext(connectionString);
        }
        public DataRepository()
        {
            _context = new LibraryDataContext();
        }
        //book
        private IBook EntryToObject(book book)
        {
            if (book == null)
            {
                return null;
            }
            return new Book()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsBorrowed = book.IsBorrowed
            };

        }

        public override void AddBook(Guid id, string title, string author, bool isBorrowed)
        {
            book book = new book()
            {
                Id = id,
                Title = title,
                Author = author,
                IsBorrowed = isBorrowed
            };
            _context.book.InsertOnSubmit(book);
            _context.SubmitChanges();
        }

        //user
        private IUser EntryToObject(user user)
        {
            if (user == null)
            {
                return null;
            }
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
            };
        }




        public override void AddUser(Guid id, string name, string surname)
        {
        user user1 = new user()
                {
                    Id = id,
                    Name = name,
                    Surname = surname
                };
                _context.user.InsertOnSubmit(user1);
                _context.SubmitChanges();
            

        }

        public override void BorrowBook(Guid userId, Guid bookId)
        {
            book currentBook = (
                from b in _context.book
                where b.Id == bookId
                select b).FirstOrDefault();


            currentBook.IsBorrowed = true;
            borrow borrow = new borrow()
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                UserId = userId,
                Date = DateTime.Now
            };
            _context.borrow.InsertOnSubmit(borrow);
            _context.SubmitChanges();
        }

        //Query syntax
        public override IBook GetBookById(Guid id)
        {
            book currentBook = (
                from b in _context.book
                where b.Id == id
                select b).FirstOrDefault();
            if (currentBook == null)
            {
                return null;
            }
            return EntryToObject(currentBook);
        }

        //method syntax
        public override List<IBook> GetNBooks(int n, int offset)
        {
            List<book> books = _context.book
                .Skip(offset)
                .Take(n)
                .ToList();
            return books
                .Select(b => EntryToObject(b))
                .ToList();
        }

        //Query syntax
        public override List<IUser> GetNUsers(int n, int offset)
        {
            List<user> users = (
                from u in _context.user
                select u).Skip(offset).Take(n).ToList(); 

            return users
                .Select(u => EntryToObject(u))
                .ToList();
        }

        public override IUser GetUserById(Guid id)
        {
            user currentUser = _context.user
                .Where(u => u.Id == id)
                .FirstOrDefault();
            if (currentUser == null)
            {
                return null;
            }
            return EntryToObject(currentUser);
        }

        public override void ReturnBook(Guid userId, Guid bookId)
        {
            book currentBook = (
                from b in _context.book
                where b.Id == bookId
                select b).FirstOrDefault();


            currentBook.IsBorrowed = false;
             returnE currentReturn = new returnE()
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                UserId = userId,
                Date = DateTime.Now
            };
            _context.returnE.InsertOnSubmit(currentReturn);
            _context.SubmitChanges();

        }

        //Return
        private IReturn EntryToObject(returnE returnE)
        {
            if (returnE == null)
            {
                return null;
            }
            return new Return()
            {
                Id = returnE.Id,
                BookId = returnE.BookId,
                UserId = returnE.UserId,
                Date = returnE.Date
            };
        }
        //Borrow
        private IBorrow EntryToObject(borrow borrow)
        {
            if (borrow == null)
            {
                return null;
            }
            return new Borrow()
            {
                Id = borrow.Id,
                BookId = borrow.BookId,
                UserId = borrow.UserId,
                Date = borrow.Date
            };
        }

        public override List<IReturn> GetNReturns(int n, int offset)
        {
            List<returnE> returns = (
                from r in _context.returnE
                select r).Skip(offset).Take(n).ToList();
            return returns
                .Select(r => EntryToObject(r))
                .ToList();
        }

        public override List<IBorrow> GetNBorrows(int n, int offset)
        {
            List<borrow> borrows = (
                from b in _context.borrow
                select b).Skip(offset).Take(n).ToList();
            return borrows
                .Select(b => EntryToObject(b))
                .ToList();
        }

        public override void DeleteBookWithEvents(Guid id)
        {
            book currentBook = (
                from b in _context.book
                where b.Id == id
                select b).FirstOrDefault();
            
            returnE currentReturn = (
                from r in _context.returnE
                where r.BookId == id
                select r).FirstOrDefault();
            borrow currentBorrow = (
                from b in _context.borrow
                where b.BookId == id
                select b).FirstOrDefault();
            _context.returnE.DeleteAllOnSubmit(_context.returnE.Where(r => r.BookId == id));
            _context.borrow.DeleteAllOnSubmit(_context.borrow.Where(b => b.BookId == id));
            _context.SubmitChanges();

            if (currentBook == null)
            {
                throw new Exception("Book not found");
            }

            _context.book.DeleteOnSubmit(currentBook);
            _context.SubmitChanges();
        }

    }
}
