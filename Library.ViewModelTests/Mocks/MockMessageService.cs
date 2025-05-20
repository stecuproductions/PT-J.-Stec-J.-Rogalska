using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Presentation;

namespace Library.ViewModelTests.Mocks
{
    internal class MockMessageService : IMessageService
    {
        public string LastMessage { get; set; }
        public void ShowMessage(string message)
        {
            LastMessage = message;
        }
    }
}
