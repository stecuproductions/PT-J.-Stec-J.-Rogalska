using LibraryProject.LibraryLogic;
using LibraryProject.LibraryUi;
using LibraryProject.LibraryData;
class Program
{
    static void Main()
    {
        //layererd architecture data->logic->UI
        LibraryRepository libraryRepository = new LibraryRepository();
        LibraryManager manager = new LibraryManager(libraryRepository);
        LibraryUi ui = new LibraryUi(manager);

        ui.Run();
    }
}