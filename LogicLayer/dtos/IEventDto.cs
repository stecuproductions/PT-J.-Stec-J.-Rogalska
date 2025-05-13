using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.dtos
{
    public interface IEventDto
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
        public Guid? BookId { get; set; }
    }
}
