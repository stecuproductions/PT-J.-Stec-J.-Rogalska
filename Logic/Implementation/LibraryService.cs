using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;
using Library.Logic.API;

namespace Library.Logic.Implementation
{
    internal class LibraryService : ILibraryService
    {
        private IDataRepository _dataRepository;

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
