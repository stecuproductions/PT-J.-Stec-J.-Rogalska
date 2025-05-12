using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public Guid? BookId { get; set; }
    }
}
