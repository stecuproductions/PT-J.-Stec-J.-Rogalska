using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.dtos;

namespace Library.Logic.dtos { 
    internal class BookDto : IBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsBorrowed { get; set; }
        public string Author { get; set; } = string.Empty;
    }
}
