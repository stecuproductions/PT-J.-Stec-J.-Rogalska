﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LibraryTests")]

namespace Library.Data
{
    internal class InMemoryDataProvider : IDataProvider
    {
        private readonly ILibraryState _state;
        private readonly List<IEvent> _events = new();

        public InMemoryDataProvider()
        {
            _state = new LibraryState 
            {
                Users = new List<IUser>(),
                Books = new List<IBook>()
            };
        }

        public ILibraryState GetLibraryState()
        {
            return _state;
        }
        public List<IEvent> GetEvents()
        {
            return _events;
        }
        public void GenerateSampleData()
        {
            var book1 = new Book { Title = "Wiedźmin", Author = "Andrzej Sapkowski" };
            var book2 = new Book { Title = "1984", Author = "George Orwell" };
            var user = new User { Name = "Kuba" };
            _state.Books.Add(book1);
            _state.Books.Add(book2);
            _state.Users.Add(user);
        }
        public void GenerateEmptyDataSet()
        {
            _state.Books.Clear();
            _state.Users.Clear();
            _events.Clear();
        }

        public void AddEvent(IUser user, IBook book, string description)
        {
            _events.Add(new Event
            {
                User = user,
                Book = book,
                Description = description,
                Timestamp = DateTime.Now
            });
        }
    }
}
