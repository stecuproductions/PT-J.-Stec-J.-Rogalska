using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.Data.API
{
    public interface IBook
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        bool IsBorrowed { get; set; }
    }
}
