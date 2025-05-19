using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;

namespace Library.LogicTests.Mocks
{
    internal class MockRepo : IDataRepository
    {
        private List<MockBook> _books = new List<MockBook>();
        private List<MockUser> _users = new List<MockUser>();
        private List<MockBorrow> _borrows = new List<MockBorrow>();
        private List<MockReturn> _returns = new List<MockReturn>();

        public override void AddBook(Guid id, string title, string author, bool isBorrowed)
        {
            _books.Add(new MockBook
            {
                Id = id,
                Title = title,
                Author = author,
                IsBorrowed = isBorrowed
            });
        }

        public override void AddUser(Guid id, string name, string surname)
        {
           _users.Add(new MockUser
           {
               Id = id,
               Name = name,
               Surname = surname
           });
        }

        public override void BorrowBook(Guid userId, Guid bookId)
        {
            _borrows.Add(new MockBorrow
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BookId = bookId,
                Date = DateTime.Now
            });
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.IsBorrowed = true;
            }

            
        }

        public override void DeleteBookWithEvents(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
                var borrowsToRemove = _borrows.Where(b => b.BookId == id).ToList();
                foreach (var borrow in borrowsToRemove)
                {
                    _borrows.Remove(borrow);
                }
                var returnsToRemove = _returns.Where(r => r.BookId == id).ToList();
                foreach (var returnEvent in returnsToRemove)
                {
                    _returns.Remove(returnEvent);
                }
            }

        }

        public override IBook GetBookById(Guid id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public override List<IBook> GetNBooks(int n, int offset)
        {
            return _books.Skip(offset).Take(n).Cast<IBook>().ToList();
        }

        public override List<IBorrow> GetNBorrows(int n, int offset)
        {
            return _borrows.Skip(offset).Take(n).Cast<IBorrow>().ToList();
        }

        public override List<IReturn> GetNReturns(int n, int offset)
        {
            return _returns.Skip(offset).Take(n).Cast<IReturn>().ToList();
        }

        public override List<IUser> GetNUsers(int n, int offset)
        {
            return _users.Skip(offset).Take(n).Cast<IUser>().ToList();
        }

        public override IUser GetUserById(Guid id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public override void ReturnBook(Guid userId, Guid bookId)
        {
            _returns.Add(new MockReturn
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BookId = bookId,
                Date = DateTime.Now
            });
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.IsBorrowed = false;
            }

        }
    }
}
