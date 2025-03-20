using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryProject.Models;

namespace LibraryProjectTests.ModelsTests
{
    [TestClass]
    public sealed class BookTests
    {
        [TestMethod]
        public void GetParameters_ShouldReturnCorrectValues()
        {
            var book = new Book(1, "Test title", "Test author");

            Assert.IsNotNull(book);
            Assert.AreEqual("Test title", book.Title);
            Assert.AreEqual("Test author", book.Author);
            Assert.AreEqual(1, book.Id);
        }
        [TestMethod]
        public void SetParameters_ShouldReturnCorrectValues()
        {
            var book = new Book(1, "Test title", "Test author");
            Assert.IsNotNull(book);
            book.SetTitle("Edited test title");
            book.SetAuthor("Edited test author");
            Assert.AreEqual("Edited test title", book.Title);
            Assert.AreEqual("Edited test author", book.Author);
            Assert.AreEqual(1, book.Id);
        }
    }
}
