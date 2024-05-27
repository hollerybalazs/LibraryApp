using Microsoft.Extensions.Logging;
using Moq;
using Library.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Tests
{
    public class BorrowServiceTests
    {
        private Mock<ILogger<BorrowService>> _loggerServiceMock;
        private Mock<LibraryContext> _libraryContextMock;
        private BorrowService _borrowService;

        public List<Borrow> borrowList = new List<Borrow>()
        {
            new Borrow
            {
                Id = Guid.NewGuid(),
                ReaderNumber = Guid.NewGuid(),
                InventoryNumber = Guid.NewGuid(),
                BorrowDate = new DateTime(2024,05,01),
                OverDueDate = new DateTime(2024,05,25)
            },
            new Borrow
            {
                Id = Guid.NewGuid(),
                ReaderNumber = Guid.NewGuid(),
                InventoryNumber = Guid.NewGuid(),
                BorrowDate = new DateTime(2024,04,04),
                OverDueDate = new DateTime(2024,05,24)
            },
            new Borrow
            {
                Id = Guid.NewGuid(),
                ReaderNumber = Guid.NewGuid(),
                InventoryNumber = Guid.NewGuid(),
                BorrowDate = new DateTime(2023,01,01),
                OverDueDate = new DateTime(2024,07,01)
            }
        };
        public BorrowServiceTests()
        {
            _loggerServiceMock = new Mock<ILogger<BorrowService>>();
            _libraryContextMock = new Mock<LibraryContext>();
        }

        [Fact]
        public async Task NonExistentBorrow_AddBorrow_ExistingBorrow()
        {
            var borrow = borrowList[0];

            _libraryContextMock.Setup(x => x.Add(borrow)).Returns(It.IsAny<EntityEntry<Borrow>>());
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Borrows.Find(borrow.InventoryNumber)).Returns(borrow);

            _borrowService = new BorrowService(_loggerServiceMock.Object, _libraryContextMock.Object);

            await _borrowService.Add(borrow);

            Assert.Equal(borrow, _libraryContextMock.Object.Borrows.Find(borrow.InventoryNumber));
        }

        [Fact]
        public async Task ExistingBorrow_DeleteBorrow_NonExistentBorrow()
        {
            var borrow = borrowList.First();

            _libraryContextMock.Setup(x => x.Remove(borrow)).Returns(It.IsAny<EntityEntry<Borrow>>());
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Borrows.Find(borrow.InventoryNumber)).Returns((Borrow)null);

            _borrowService = new BorrowService(_loggerServiceMock.Object, _libraryContextMock.Object);

            await _borrowService.Delete(borrow.InventoryNumber);

            Assert.Null(_libraryContextMock.Object.Borrows.Find(borrow.InventoryNumber));
        }

        [Fact]
        public async Task ExistingBorrow_GetBorrow_GotBorrow()
        {
            var borrow = borrowList.First();

            _libraryContextMock.Setup(x => x.Borrows.FindAsync(borrow.Id)).ReturnsAsync(borrow);

            _borrowService = new BorrowService(_loggerServiceMock.Object, _libraryContextMock.Object);

            var result = await _borrowService.Get(borrow.Id);   

            Assert.Equal(borrow, result);
        }

        /*
        [Fact]
        public async Task ExistingBooks_GetAll_GotAll()
        {
            var books = bookList; 
            _libraryContextMock.Setup(x => x.).Returns(books.AsQueryable());
            _bookService = new BookService(_loggerServiceMock.Object, _libraryContextMock.Object);

            var result = await _bookService.GetAll();

            Assert.Equal(books, result);
        }
        */

        [Fact]
        public async Task ExistingBorrow_UpdateBorrow_UpdatedBorrow()
        {
            var originalBorrow = borrowList.First();
            var updatedBorrow = new Borrow
            {
                Id = originalBorrow.Id,
                ReaderNumber = Guid.NewGuid(),
                InventoryNumber = Guid.NewGuid(),
                BorrowDate = new DateTime(2024, 05, 01),
                OverDueDate = new DateTime(2024, 06, 25)
            };

            _libraryContextMock.Setup(x => x.Borrows.FindAsync(originalBorrow.Id)).ReturnsAsync(originalBorrow);

            _borrowService = new BorrowService(_loggerServiceMock.Object, _libraryContextMock.Object);

            _libraryContextMock.Setup(x => x.Borrows.Find(updatedBorrow.Id)).Returns(originalBorrow);
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Borrows.Update(originalBorrow)).Returns(It.IsAny<EntityEntry<Borrow>>());

            await _borrowService.Update(originalBorrow);

            Assert.Equal(originalBorrow.Id, _libraryContextMock.Object.Borrows.Find(originalBorrow.Id).Id);
            Assert.Equal(originalBorrow.ReaderNumber, _libraryContextMock.Object.Borrows.Find(originalBorrow.Id).ReaderNumber);
            Assert.Equal(originalBorrow.InventoryNumber, _libraryContextMock.Object.Borrows.Find(originalBorrow.Id).InventoryNumber);
            Assert.Equal(originalBorrow.BorrowDate, _libraryContextMock.Object.Borrows.Find(originalBorrow.Id).BorrowDate);
            Assert.Equal(originalBorrow.OverDueDate, _libraryContextMock.Object.Borrows.Find(originalBorrow.Id).OverDueDate);
        }
    }
}
