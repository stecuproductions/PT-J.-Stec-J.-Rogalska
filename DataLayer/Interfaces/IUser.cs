﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }
        List<IBook> BorrowedBooks { get; }

    }
}
