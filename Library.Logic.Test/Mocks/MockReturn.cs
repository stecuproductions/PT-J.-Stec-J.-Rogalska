using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.API;

namespace Library.LogicTests.Mocks
{
    internal class MockReturn : IReturn
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
