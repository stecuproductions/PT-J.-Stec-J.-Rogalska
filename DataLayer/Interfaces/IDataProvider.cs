using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IDataProvider
    {
        ILibraryState GetLibraryState();
        List<IEvent> GetEvents();
        void GenerateSampleData();
        void GenerateEmptyDataSet();
        void AddEvent(IUser user, IBook book, string description);
    }
}
