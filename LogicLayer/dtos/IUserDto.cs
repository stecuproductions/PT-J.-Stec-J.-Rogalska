using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.dtos
{
    public interface IUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
