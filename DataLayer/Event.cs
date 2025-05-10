using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    internal class Event : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public IUser User { get; set; } = null!;
        public IBook Book { get; set; } = null!;
    }
}
