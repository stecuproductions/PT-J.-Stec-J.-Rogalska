using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.Model.API
{
    public interface IUserModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
    }
}
