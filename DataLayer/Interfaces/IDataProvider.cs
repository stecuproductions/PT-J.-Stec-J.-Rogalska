using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public interface IDataProvider
    {
        List<IEvent> GetEvents();
        void GenerateEmptyDataSet();
        void AddEvent(IUser user, IBook book, string description);
        ILibraryState GetLibraryState();
    }
}
