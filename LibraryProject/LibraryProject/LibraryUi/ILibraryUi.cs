using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.LibraryUi
{
    interface ILibraryUi
    {
        void Run();
        void ShowBooksUI();
        void AddBookUI();
        void EditBookUI();
        void DeleteBookUI();

    }
}
