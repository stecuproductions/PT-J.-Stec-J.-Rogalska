using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace LibraryTests.TestUtils
{
    internal class SampleDataProvider : InMemoryDataProvider
    {
        public void GenerateSampleData()
        {
            var book1 = new Book { Title = "Wiedźmin", Author = "Andrzej Sapkowski" };
            var book2 = new Book { Title = "1984", Author = "George Orwell" };
            var user = new User("Kuba");
            _state.Books.Add(book1);
            _state.Books.Add(book2);
            _state.Users.Add(user);
        }
    }
}
