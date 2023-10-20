﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObject;
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
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }


        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
           var books = _manager.Book.GetAllBooks(trackChanges);

           return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public BookDto GetOneBookById(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(trackChanges,id);
            if (book == null)
            {
                throw new BookNotFoundException(id);
            }
            return _mapper.Map<BookDto>(book);
        }

        public BookDto CreateOneBook(BookDtoForInsert bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);

            _manager.Book.CreateOneBook(entity);
            _manager.Save();
            return _mapper.Map<BookDto>(entity);
        }

        public void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(trackChanges,id);
            //check entity
            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            //check params
            if (bookDto is null)
            {
                throw new ArgumentNullException(nameof(bookDto));
            }
            //mapping
            //entity.Title = book.Title;
            //entity.Price = book.Price;
            entity = _mapper.Map<Book>(bookDto);

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

        public (BookDtoForUpdate bookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(trackChanges, id);
            if(book is null)
                throw new BookNotFoundException(id);
            
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);

            return (bookDtoForUpdate, book);
                
        }

        public void SaveChangesForPatch(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate,book);
            _manager.Save();
        }
    }
}
