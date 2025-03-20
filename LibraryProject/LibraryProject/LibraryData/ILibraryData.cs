using System.Collections.Generic;
using LibraryProject.Models;

namespace LibraryProject.LibraryData
{
    public interface ILibraryRepository
    {
        void AddBookToData(int id, string title, string author);
        void DeleteBookFromData(int id);
        void EditBookInData(int id, string title, string author);

        Book GetBookByIdFromData(int id);
        List<Book> GetBooksFromData();

    }
}
