using Library.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;


namespace Library.Tests
{
    public class BookServiceTests
    {
        private Mock<ILogger<BookService>> _loggerServiceMock;
        private Mock<LibraryContext> _libraryContextMock;
        private BookService _bookService;

        public List<Book> bookList = new List<Book>()
        {
            new Book
            {
                InventoryNumber = Guid.NewGuid(),
                Title = "A P�l utcai fi�k",
                Author = "Ferenc Moln�r",
                Publisher = "Eur�pa K�nyvkiad�",
                Date = DateTime.Now,
            },

            new Book
            {
                InventoryNumber = Guid.NewGuid(),
                Title = "Az arany ember",
                Author = "M�r J�kai",
                Publisher = "Helikon Kiad�",
                Date = DateTime.Now,
            },

            new Book
            {
                InventoryNumber = Guid.NewGuid(),
                Title = "Egri csillagok",
                Author = "G�za G�rdonyi",
                Publisher = "Eur�pa K�nyvkiad�",
                Date = DateTime.Now,
            }
        };

        public BookServiceTests()
        {
            _loggerServiceMock = new Mock<ILogger<BookService>>();
            _libraryContextMock = new Mock<LibraryContext>();
        }

        [Fact]
        public async Task NonExistentBook_AddBook_ExistingBook()
        {
            var book = bookList[0];

            _libraryContextMock.Setup(x => x.Add(book)).Returns(It.IsAny<EntityEntry<Book>>());
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Books.Find(book.InventoryNumber)).Returns(book);

            _bookService = new BookService(_loggerServiceMock.Object, _libraryContextMock.Object);

            await _bookService.Add(book);

            Assert.Equal(book, _libraryContextMock.Object.Books.Find(book.InventoryNumber));
        }


        [Fact]
        public async Task ExistingBook_DeleteBook_NonExistentBook()
        {
            var book = bookList.First();

            _libraryContextMock.Setup(x => x.Remove(book)).Returns(It.IsAny<EntityEntry<Book>>());
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Books.Find(book.InventoryNumber)).Returns((Book)null);

            _bookService = new BookService(_loggerServiceMock.Object, _libraryContextMock.Object);

            await _bookService.Delete(book.InventoryNumber);

            Assert.Null(_libraryContextMock.Object.Books.Find(book.InventoryNumber));
        }

        [Fact]
        public async Task ExistingBook_GetBook_GotBook()
        {
            var book = bookList.First();

            _libraryContextMock.Setup(x => x.Books.FindAsync(book.InventoryNumber)).ReturnsAsync(book);

            _bookService = new BookService(_loggerServiceMock.Object, _libraryContextMock.Object);

            var result = await _bookService.Get(book.InventoryNumber);

            Assert.Equal(book, result);
        }

        [Fact]
        public async Task ExistingBook_UpdateBook_UpdatedBook()
        {
            var originalBook = bookList.First();
            var updatedBook = new Book
            {
                InventoryNumber = originalBook.InventoryNumber,
                Title = "A P�l utcai fi�k",
                Author = "Moln�r Ferenc",
                Publisher = "M�ra Ferenc Ifj�s�gi K�nyvkiad�",
                Date = DateTime.Now
            };

            _libraryContextMock.Setup(x => x.Books.FindAsync(originalBook.InventoryNumber)).ReturnsAsync(originalBook);

            _bookService = new BookService(_loggerServiceMock.Object, _libraryContextMock.Object);

            _libraryContextMock.Setup(x => x.Books.Find(updatedBook.InventoryNumber)).Returns(originalBook);
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Books.Update(updatedBook)).Returns(It.IsAny<EntityEntry<Book>>());

            await _bookService.Update(updatedBook);

            Assert.Equal(updatedBook.InventoryNumber, _libraryContextMock.Object.Books.Find(originalBook.InventoryNumber).InventoryNumber);
            Assert.Equal(updatedBook.Title, _libraryContextMock.Object.Books.Find(originalBook.InventoryNumber).Title);
            Assert.Equal(updatedBook.Author, _libraryContextMock.Object.Books.Find(originalBook.InventoryNumber).Author);
            Assert.Equal(updatedBook.Publisher, _libraryContextMock.Object.Books.Find(originalBook.InventoryNumber).Publisher);
            Assert.Equal(updatedBook.Date, _libraryContextMock.Object.Books.Find(originalBook.InventoryNumber).Date);
        }
    }
}
