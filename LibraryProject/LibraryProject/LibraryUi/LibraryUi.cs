using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryProject.LibraryUi
{
    class LibraryUi : ILibraryUi
    {
        private readonly LibraryProject.LibraryLogic.LibraryManager libraryManager;

        public LibraryUi(LibraryProject.LibraryLogic.LibraryManager manager)
        {
            libraryManager = manager;
        }
        public void Run()
        {
            Console.WriteLine("Welcome to the Library Manager!\n");
            while (true)

            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Choose action");
                Console.WriteLine("1. Show books");
                Console.WriteLine("2. Add book");
                Console.WriteLine("3. Edit book");
                Console.WriteLine("4. Delete book");
                string userInput =  Console.ReadLine();
                
                switch (userInput)
                {
                    case "1":
                        ShowBooksUI();
                        break;
                    case "2":
                        AddBookUI();
                        break;
                    case "3":
                        EditBookUI();
                        break;
                    case "4":
                        DeleteBookUI();
                    break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
                Console.WriteLine("-----------------------------");


            }
        }
        public void ShowBooksUI() {
            Console.WriteLine("Showing books");
            libraryManager.ShowBooks();

        }
        public void AddBookUI()
        {
            Console.WriteLine("Adding book");
            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Author name: ");
            string author = Console.ReadLine();
            bool result = libraryManager.AddBook(title, author);
            if (result)
            {
                Console.WriteLine("Book added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add book");
            }
        }
        public void EditBookUI()
        {
            Console.WriteLine("Editing book");
            Console.WriteLine("Input a book id");
            string id = Console.ReadLine();
            Console.WriteLine("Input an edited book title");
            string title = Console.ReadLine();
            Console.WriteLine("Input an edited book author");
            string author = Console.ReadLine();
            bool result = libraryManager.EditBook(id, title, author);
            if (result)
            {
                Console.WriteLine("Book edited successfully");
            }
            else 
            {
                Console.WriteLine("Failed to edit book");   
            }
        }
        public void DeleteBookUI()
        {
            Console.WriteLine("Deleting book");
            Console.WriteLine("Input an id of the book to delete: ");
            string id = Console.ReadLine();
            bool result = libraryManager.DeleteBook(id);
            if (result)
            {
                Console.WriteLine("Book deleted successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete book");
            }
        }

    }
}
