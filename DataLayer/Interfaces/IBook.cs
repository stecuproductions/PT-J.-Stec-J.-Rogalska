using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IBook
    {
        Guid Id { get; }
        string Title { get; }
        string Author { get; }
        bool IsBorrowed { get; set; }

    }
}
