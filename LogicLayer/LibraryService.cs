﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data;

namespace Library.Logic;

public class LibraryService
{
    private readonly IDataProvider _dataProvider;

    public LibraryService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public bool BorrowBook(Guid userId, Guid bookId)
    {
        var state = _dataProvider.GetLibraryState();
        var user = state.Users.FirstOrDefault(u => u.Id == userId);
        var book = state.Books.FirstOrDefault(b => b.Id == bookId);

        if (user == null || book == null || book.IsBorrowed)
            return false;

        book.IsBorrowed = true;
        user.BorrowedBooks.Add(book);

        var description = $"User '{user.Name}' borrowed '{book.Title}'";

        _dataProvider.AddEvent(user, book, description);

        return true;
    }

    public bool ReturnBook(Guid userId, Guid bookId)
    {
        var state = _dataProvider.GetLibraryState();
        var user = state.Users.FirstOrDefault(u => u.Id == userId);
        var book = state.Books.FirstOrDefault(b => b.Id == bookId);

        if (user == null || book == null || !book.IsBorrowed)
            return false;

        book.IsBorrowed = false;
        user.BorrowedBooks.Remove(book);

        var description = $"User '{user.Name}' returned '{book.Title}'";

        _dataProvider.AddEvent(user, book, description);

        return true;
    }

    public List<IBook> GetAvailableBooks()
    {
        return _dataProvider
            .GetLibraryState()
            .Books
            .Where(book => !book.IsBorrowed)
            .ToList();
    }
}
