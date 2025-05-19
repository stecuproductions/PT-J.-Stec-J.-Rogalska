using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.API
{
    public interface IBorrowLogic
    {
        Guid Id { get; set; }
        Guid BookId { get; set; }
        Guid UserId { get; set; }
        DateTime Date { get; set; }
    }
}
