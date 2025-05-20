using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.API;

namespace Library.ViewModelTests.Mocks
{
    internal class MockBook : IBookLogic
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
    }
}
