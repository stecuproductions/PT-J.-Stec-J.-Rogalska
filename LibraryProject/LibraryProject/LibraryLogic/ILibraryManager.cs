using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.LibraryLogic
{
    interface ILibraryManager
    {
        string ShowBooks(); //zwraca Book.toString() dla kazdej ksiazki. Mozesz uzyc for (book in Books) result+=book.toString() return result; 
        bool EditBook(string id, string title, string desc); //bierze id od uzytkownika i edytuje ksiazke na danym id + weryfikacja czy id sie zgadza (musi byc int)
        bool DeleteBook(string id); //usuwa ksiazke z danego id  + weryfikacja czy id sie zgadza (musi byc int)
        bool AddBook( string title, string desc); //dodaje ksiazke, musisz okreslic id recznie. Propponuje zrobic to na zasadzie dlugosci listy ksiazek

    }
}
