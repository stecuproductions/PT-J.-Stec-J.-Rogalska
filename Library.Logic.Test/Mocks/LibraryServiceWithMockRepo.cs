using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logic.Implementation;
using Library.Data.API;
using Library.Logic.API;
using Library.Data.Implementation;
using Library.Logic.Implementation;

namespace Library.LogicTests.Mocks
{
    internal class LibraryServiceWithMockRepo: LibraryService
    {
        public LibraryServiceWithMockRepo(MockRepo repo) : base(repo)
        {
        }


    }
}
