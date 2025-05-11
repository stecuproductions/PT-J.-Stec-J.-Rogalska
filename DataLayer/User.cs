using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    internal class User : IUser
    {
        public User(string name)
        {
            Id = new Guid();
            Name = name;
            BorrowedBooks = new List<IBook>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<IBook> BorrowedBooks { get; set; } = new();

    }
}
