using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.dtos { 
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsBorrowed { get; set; }
        public string Author { get; set; } = string.Empty;
    }
}
