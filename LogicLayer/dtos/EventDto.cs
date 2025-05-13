using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.dtos;

namespace Library.Logic.dtos
{
    internal class EventDto : IEventDto
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public Guid? BookId { get; set; }
    }
}
