using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    internal static class SqlMapper
    {
        public static Event DbEventToModelEvent(DbEvent dbEvt)
        {
            return new Event
            {
                Id = dbEvt.Id,
                Timestamp = dbEvt.Timestamp,
                Description = dbEvt.Description,
                UserId = dbEvt.DbUser != null ? dbEvt.DbUser.Id : (Guid?)null,
                BookId = dbEvt.DbBook != null ? dbEvt.DbBook.Id : (Guid?)null

            };
        }

        public static DbEvent ModelEventToDbEvent(IEvent modelEvt)
        {
            return new DbEvent
            {
                Id = modelEvt.Id,
                Timestamp = modelEvt.Timestamp,
                Description = modelEvt.Description,
                UserId = modelEvt.UserId,
                BookId = modelEvt.BookId
            };
        }


        public static Book DbBookToModelBook(DbBook dbBook)
        {
            return new Book
            {
                Id = dbBook.Id,
                Title = dbBook.Title,
                Author = dbBook.Author,
                IsBorrowed = dbBook.IsBorrowed
            };
        }

        public static DbBook ModelBookToDbBook(IBook modelBook)
        {
            return new DbBook
            {
                Id = modelBook.Id,
                Title = modelBook.Title,
                Author = modelBook.Author,
                IsBorrowed = modelBook.IsBorrowed
            };
        }

        public static User DbUserToModelUser(DbUser dbUser)
        {
            return new User
            {
                Id = dbUser.Id,
                Name = dbUser.Name
            };
        }
        public static DbUser ModelUserToDbUser(IUser modelUser)
        {
            return new DbUser
            {
                Id = modelUser.Id,
                Name = modelUser.Name
            };
        }
    }
}
