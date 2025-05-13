using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.dtos;

namespace Library.Logic.dtos
{
    internal class UserDto : IUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
