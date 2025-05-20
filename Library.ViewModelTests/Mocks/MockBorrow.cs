using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.API;

namespace Library.ViewModelTests.Mocks
{
    internal class MockBorrow : IBorrowLogic
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
