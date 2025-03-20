using System;
using System.Collections.Generic;
using System.Linq;
using LibraryProject.LibraryData;
using LibraryProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryProjectTests.DataLayerTests
{
    [TestClass]
    public sealed class LibraryDataTest
    {
        [TestMethod]
        public void DataInsertionAndManipulation_ShouldReturnPositiveValues()
        {
            // Arrange
            LibraryRepository testRepo = new LibraryRepository();
            var booksList = testRepo.GetBooksFromData();
            Assert.IsNotNull(booksList);

            // Act - Adding book
            testRepo.AddBookToData(0, "Harry Potter", "J.K Rowling");
            //Getting book by ID
            var addedBook = testRepo.GetBookByIdFromData(0);
            Assert.AreSame(addedBook, testRepo.GetBookByIdFromData(0));

            
            // Assert - Check if the book is added correctly
            Assert.IsNotNull(addedBook, "Książka nie została dodana.");
            Assert.AreEqual("J.K Rowling", addedBook.Author);
            Assert.AreEqual("Harry Potter", addedBook.Title);
            Assert.AreEqual(0, addedBook.Id);

            // Act - Editing book
            testRepo.EditBookInData(0, "Harry Potter", "Edited author name");
            var editedBook = testRepo.GetBookByIdFromData(0);

            // Assert - Check if the book was edited correctly
            Assert.IsNotNull(editedBook, "Książka nie została znaleziona po edycji.");
            Assert.AreEqual("Edited author name", editedBook.Author);
            Assert.AreEqual("Harry Potter", editedBook.Title);
            Assert.AreEqual(0, editedBook.Id);

            // Act - Deleting book
            testRepo.DeleteBookFromData(0);
            var deletedBook = testRepo.GetBookByIdFromData(0);

            // Assert - Check if the book was deleted correctly
            Assert.IsNull(deletedBook);
        }
    }
}