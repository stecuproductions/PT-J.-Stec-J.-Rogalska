using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;

namespace Library.LogicTests.Mocks
{
    internal class MockBook : IBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
