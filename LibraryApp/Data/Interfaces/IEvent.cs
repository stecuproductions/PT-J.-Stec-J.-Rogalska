using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data;

public interface IEvent
{
    Guid Id { get; }
    DateTime Timestamp { get; }
    string Description { get; }
    IUser User { get; }
    IBook Book { get; }
}