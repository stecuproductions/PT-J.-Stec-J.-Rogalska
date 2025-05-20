using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.API;

namespace Library.ViewModelTests.Mocks
{
    public class MockLibraryService : ILibraryService
    {
        private List<IBookLogic> _books = new();
        private List<IBorrowLogic> _borrows = new();
        private List<IReturnLogic> _returns = new();
        public override bool AddBookLogic(string title, string author)
        {
            _books.Add(new MockBook
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                IsBorrowed = false
            });
            return true;
        }

        public override bool AddUserLogic(string name, string surname) => true;

        public override bool BorrowBookLogic(Guid userId, Guid bookId)
        {
            IBookLogic book = _books.Find(b => b.Id == bookId);
            if(book !=null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                _borrows.Add(new MockBorrow
                {
                    Id = Guid.NewGuid(),
                    BookId = bookId,
                    UserId = userId,
                    Date = DateTime.Now
                });
                return true;
            }
            return false;
        }

        public override bool DeleteBookWithEventsLogic(Guid bookId)
        {
            IBookLogic book = _books.Find(b => b.Id==bookId);
            if(book != null)
            {
                _books.Remove(book);
                return true;
            }
            return false;
        }

        public override IBookLogic GetBookByIdLogic(Guid id) => _books.Find(b => b.Id == id);

        public override IEnumerable<IBookLogic> GetNBooksLogic(int n, int offset) => _books.Skip(offset).Take(n);

        public override IEnumerable<IBorrowLogic> GetNBorrowsLogic(int n, int offset) => _borrows.Skip(offset).Take(n);

        public override IEnumerable<IReturnLogic> GetNReturnsLogic(int n, int offset) => _returns.Skip(offset).Take(n);

        public override IEnumerable<IUserLogic> GetNUsersLogic(int n, int offset) => new List<IUserLogic>();

        public override bool ReturnBookLogic(Guid userId, Guid bookId)
        {
            IBookLogic book = _books.Find(b => b.Id == bookId);
            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                _returns.Add(new MockReturn
                {
                    Id = Guid.NewGuid(),
                    BookId = bookId,
                    UserId = userId,
                    Date = DateTime.Now
                });
                return true;
            }
            return false;
        }
    }
}
