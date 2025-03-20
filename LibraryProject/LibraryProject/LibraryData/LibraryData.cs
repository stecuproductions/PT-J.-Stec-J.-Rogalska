using System.Collections.Generic;
using LibraryProject.Models;

namespace LibraryProject.LibraryData
{
    public class LibraryRepository : ILibraryRepository
    {
        private List<Book> books = new List<Book>();

        public void AddBookToData(int id, string title, string author)
        {
            books.Add(new Book(id, title, author));
        }
        public void DeleteBookFromData(int id)
        {
            books.RemoveAt(id);
        }

        public void EditBookInData(int id, string title, string author)
        {
            books[id].SetTitle(title);
            books[id].SetAuthor(author);
        }
        public List<Book> GetBooksFromData()
        {
            return books;
        }
        public Book GetBookByIdFromData(int id)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Id == id)
                {
                    return books[i];
                }

            }
            return null;

        }
    }
}
