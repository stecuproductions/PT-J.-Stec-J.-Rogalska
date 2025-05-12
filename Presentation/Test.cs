using Library.Logic;
namespace Library.Presentation
{
    public class Test
    {
        private ILibraryService libraryService;
        public Test( ILibraryService service) { 
            libraryService = service;
        }
        public void run() { 
            var books = libraryService.GetBooks();
            Console.WriteLine($"Welcome! You have '{books.Count}' books.");
            
        }
    }
}
