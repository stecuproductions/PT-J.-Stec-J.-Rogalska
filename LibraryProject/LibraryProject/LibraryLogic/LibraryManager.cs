using System;
using System.Collections.Generic;
using LibraryProject.LibraryData;

namespace LibraryProject.LibraryLogic
{
    public class LibraryManager : ILibraryManager
    {
        private readonly ILibraryRepository _libraryRepository;
        public LibraryManager(ILibraryRepository repo) {
            _libraryRepository = repo;
        }
        public string ShowBooks()
        {
            string result = "";
            //logika odwolaj sie do LibraryData.GetBooksFromData()
            return result;
        }
        //Funkcje ktore zwracaja bool maja zwracac true jak sie wszystko uda
        public bool EditBook(string id, string title, string desc)
        {
            return true; 
            //logika odwolaj sie do LibraryData.EditBookInData(int id, string title, string author)

        }
        public bool DeleteBook(string id)
        {
            return true;
            //logika odwolaj sie do LibraryData.public void DeleteBookFromData(int id)
        }
        public bool AddBook(string title, string desc)
        {
            //logika odwolaj sie do LibraryData.AddBookToData(int id, string title, string author)
            return true;
        }

    }
}
