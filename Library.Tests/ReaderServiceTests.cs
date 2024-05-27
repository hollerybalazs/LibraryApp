using Microsoft.Extensions.Logging;
using Moq;
using Library.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Library.Tests
{
    public class ReaderServiceTests
    {
        private Mock<ILogger<ReaderService>> _loggerServiceMock;
        private Mock<LibraryContext> _libraryContextMock;
        private ReaderService _readerService;

        public List<Reader> readerList = new List<Reader>()
        {
            new Reader
            {
                ReaderNumber = Guid.NewGuid(),
                Name = "Balázs Szabó",
                Address = "Alma street 1.",
                Date = new DateTime(1988, 05, 06)
            },
            new Reader
            {
                ReaderNumber = Guid.NewGuid(),
                Name = "Péter Szabó",
                Address = "Körte street 4.",
                Date = new DateTime(1999, 07, 02)
            },
            new Reader
            {
                ReaderNumber = Guid.NewGuid(),
                Name = "Anna Szabó",
                Address = "Cseresznye street 17.",
                Date = new DateTime(1988, 11, 11)
            }
        };

        public ReaderServiceTests()
        {
            _loggerServiceMock = new Mock<ILogger<ReaderService>>();
            _libraryContextMock = new Mock<LibraryContext>();
        }

        [Fact]
        public async Task NonExistentReader_AddReader_ExistentReader()
        {
            var reader = readerList.First();

            _libraryContextMock.Setup(x => x.Add(reader)).Returns(It.IsAny<EntityEntry<Reader>>());
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Readers.Find(reader.ReaderNumber)).Returns(reader);

            _readerService = new ReaderService(_loggerServiceMock.Object, _libraryContextMock.Object);

            await _readerService.Add(reader);

            Assert.Equal(reader, _libraryContextMock.Object.Readers.Find(reader.ReaderNumber));
        }

        [Fact]
        public async Task ExistingReader_DeleteReader_NonExistentReader()
        {
            var reader = readerList.First();

            _libraryContextMock.Setup(x => x.Remove(reader)).Returns(It.IsAny<EntityEntry<Reader>>());
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Readers.Find(reader.ReaderNumber)).Returns((Reader)null);

            _readerService = new ReaderService(_loggerServiceMock.Object, _libraryContextMock.Object);

            await _readerService.Delete(reader.ReaderNumber);
            Assert.Null(_libraryContextMock.Object.Readers.Find(reader.ReaderNumber));
        }

        [Fact]
        public async Task ExistingReader_GetReader_GotReader()
        {
            var reader = readerList.First();

            _libraryContextMock.Setup(x => x.Readers.FindAsync(reader.ReaderNumber)).ReturnsAsync(reader);

            _readerService = new ReaderService(_loggerServiceMock.Object, _libraryContextMock.Object);

            var result = await _readerService.Get(reader.ReaderNumber);

            Assert.Equal(reader, result);
        }

        /*
        [Fact]
        public async Task ExistingReader_GetAll_GotAll()
        {
            var readers = readerList; 
            _libraryContextMock.Setup(x => x.).Returns(readers.AsQueryable());
            _readerService = new ReaderService(_loggerServiceMock.Object, _libraryContextMock.Object);

            var result = await _readerService.GetAll();

            Assert.Equal(readers, result);
        }
        */

        [Fact]
        public async Task ExistingReader_UpdateReader_UpdatedReader()
        {
            var originalReader = readerList.First();
            var updatedReader = new Reader
            {
                ReaderNumber = originalReader.ReaderNumber,
                Name = "Balázs Szabó",
                Address = "Barack street 1.",
                Date = new DateTime(1988, 05, 06)
            };

            _libraryContextMock.Setup(x => x.Readers.FindAsync(originalReader.ReaderNumber)).ReturnsAsync(originalReader);

            _readerService = new ReaderService(_loggerServiceMock.Object, _libraryContextMock.Object);

            _libraryContextMock.Setup(x => x.Readers.Find(originalReader.ReaderNumber)).Returns(originalReader);
            _libraryContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _libraryContextMock.Setup(x => x.Readers.Update(updatedReader)).Returns(It.IsAny<EntityEntry<Reader>>());

            await _readerService.Update(updatedReader);

            Assert.Equal(updatedReader.ReaderNumber, _libraryContextMock.Object.Readers.Find(originalReader.ReaderNumber).ReaderNumber);
            Assert.Equal(updatedReader.Name, _libraryContextMock.Object.Readers.Find(originalReader.ReaderNumber).Name);
            Assert.Equal(updatedReader.Address, _libraryContextMock.Object.Readers.Find(originalReader.ReaderNumber).Address);
            Assert.Equal(updatedReader.Date, _libraryContextMock.Object.Readers.Find(originalReader.ReaderNumber).Date);
        }

    }
}
