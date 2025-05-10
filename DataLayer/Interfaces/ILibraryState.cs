using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface ILibraryState
    {
        List<IBook> Books { get; }
        List<IUser> Users { get; }
    }
}
