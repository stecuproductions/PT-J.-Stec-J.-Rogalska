using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;
using Library.Logic.API;
[assembly: InternalsVisibleTo("Library.LogicTests")]
namespace Library.Logic.Implementation
{
    public class LibraryService : ILibraryService
    {
        protected IDataRepository _dataRepository;
        
        public LibraryService(IDataRepository dataRepo)
        {
            _dataRepository = dataRepo ?? throw new ArgumentNullException(nameof(dataRepo));
        }

        public override bool AddBookLogic(string title, string author)
        {
            try
            {
                Guid newGuid = Guid.NewGuid();
                _dataRepository.AddBook(newGuid, title, author, false);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool AddUserLogic(string name, string surname)
        {
            try
            {
                Guid newGuid = Guid.NewGuid();
                _dataRepository.AddUser(newGuid, name, surname);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool BorrowBookLogic(Guid userId, Guid bookId)
        {
            try
            {
                IBook book = _dataRepository.GetBookById(bookId);
                if (book == null)
                {
                    return false;
                }
                if (book.IsBorrowed)
                {
                    return false;
                }
                _dataRepository.BorrowBook(userId, bookId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool DeleteBookWithEventsLogic(Guid bookId)
        {
            try
            {
                IBook book = _dataRepository.GetBookById(bookId);
                if (book == null)
                {
                    return false;
                }
                _dataRepository.DeleteBookWithEvents(bookId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override IBookLogic GetBookByIdLogic(Guid id)
        {
            try
            {
                IBook book = _dataRepository.GetBookById(id);
                if (book == null)
                {
                    return null;
                }
                BookLogic bookLogic = new BookLogic()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    IsBorrowed = book.IsBorrowed
                };
                return bookLogic;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override IEnumerable<IBookLogic> GetNBooksLogic(int n, int offset)
        {
            try
            {
                List<IBook> books = _dataRepository.GetNBooks(n, offset);
                List<IBookLogic> booksLogic = new List<IBookLogic>();
                foreach (IBook book in books)
                {
                    if (book == null)
                    {
                        break;
                    }
                    BookLogic bookLogic = new BookLogic()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        IsBorrowed = book.IsBorrowed
                    };
                    booksLogic.Add(bookLogic);
                }
                return booksLogic;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override IEnumerable<IBorrowLogic> GetNBorrowsLogic(int n, int offset)
        {
            try
            {
                List<IBorrow> borrows = _dataRepository.GetNBorrows(n, offset);
                List<IBorrowLogic> borrowsLogic = new List<IBorrowLogic>();
                foreach (IBorrow borrow in borrows)
                {
                    if (borrow == null)
                    {
                        break;
                    }
                    BorrowLogic borrowLogic = new BorrowLogic()
                    {
                        Id = borrow.Id,
                        BookId = borrow.BookId,
                        UserId = borrow.UserId,
                        Date = borrow.Date
                    };
                    borrowsLogic.Add(borrowLogic);
                }
                return borrowsLogic;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override IEnumerable<IReturnLogic> GetNReturnsLogic(int n, int offset)
        {
            try
            {
                List<IReturn> returns = _dataRepository.GetNReturns(n, offset);
                List<IReturnLogic> returnsLogic = new List<IReturnLogic>();
                foreach (IReturn returnEvent in returns)
                {
                    if (returnEvent == null)
                    {
                        break;
                    }
                    ReturnLogic returnLogic = new ReturnLogic()
                    {
                        Id = returnEvent.Id,
                        BookId = returnEvent.BookId,
                        UserId = returnEvent.UserId,
                        Date = returnEvent.Date
                    };
                    returnsLogic.Add(returnLogic);
                }
                return returnsLogic;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override IEnumerable<IUserLogic> GetNUsersLogic(int n, int offset)
        {
            try
            {
                List<IUser> users = _dataRepository.GetNUsers(n, offset);
                List<IUserLogic> usersLogic = new List<IUserLogic>();
                foreach (IUser user in users)
                {
                    if (user == null)
                    {
                        break;
                    }
                    UserLogic userLogic = new UserLogic()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname
                    };
                    usersLogic.Add(userLogic);
                }
                return usersLogic;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override bool ReturnBookLogic(Guid userId, Guid bookId)
        {
            try
            {
                IBook book = _dataRepository.GetBookById(bookId);
                if (book == null)
                {
                    return false;
                }
                if (!book.IsBorrowed)
                {
                    return false;
                }
                _dataRepository.ReturnBook(userId, bookId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
