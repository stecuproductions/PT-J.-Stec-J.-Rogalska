using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Presentation.Model.API;

namespace Library.Presentation.Model.Implementation
{
    internal class ReturnModel : IReturnModel
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
