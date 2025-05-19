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
            throw new NotImplementedException();
        }

        public override IEnumerable<IBookLogic> GetNBooksLogic(int n, int offset)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IUserLogic> GetNUsersLogic(int n, int offset)
        {
            throw new NotImplementedException();
        }

        public override bool ReturnBookLogic(Guid userId, Guid bookId)
        {
            throw new NotImplementedException();
        }
    }
}
