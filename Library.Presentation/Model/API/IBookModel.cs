using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.Model.API
{
    public interface IBookModel
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        bool IsBorrowed { get; set; }
    }
}
