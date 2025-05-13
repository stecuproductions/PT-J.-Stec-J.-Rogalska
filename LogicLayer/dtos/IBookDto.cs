using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.dtos
{
    public interface IBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsBorrowed { get; set; }
        public string Author { get; set; }
    }
}
