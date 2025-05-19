﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.API
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
    }
}
