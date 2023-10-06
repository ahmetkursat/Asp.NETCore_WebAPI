using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }


        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(trackChanges,id);
            if (book == null)
            {
                throw new BookNotFoundException(id);
            }
            return book;
        }

        public Book CreateOneBook(Book book)
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(trackChanges,id);
            //check entity
            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            //check params
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            entity.Title = book.Title;
            entity.Price = book.Price;

            _manager.Book.Update(entity);
            _manager.Save();

        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(trackChanges, id);
            if (entity is null)
            {
                string message = $"The book with id :{id} could not found";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }
    }
}
