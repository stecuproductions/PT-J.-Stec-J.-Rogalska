using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class LibraryState : ILibraryState
    {
        public List<IUser> Users { get; set; }
        public List<IBook> Books { get; set; }
    }
}
