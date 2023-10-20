using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id)
        {
            var book = _manager.BookService.GetOneBookById(id, false);

            return Ok(book);
        }
    

    [HttpPost]
    public IActionResult CreateOneBooks([FromBody] BookDtoForInsert bookDto)
    {

        if (bookDto is null)
        {
            return BadRequest();
        }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


        var book = _manager.BookService.CreateOneBook(bookDto);


        return StatusCode(201, book);

    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
    {

        if (bookDto is null)
        {
            return BadRequest();//400
        }

        if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);//404
            }

        _manager.BookService.UpdateOneBook(id, bookDto , false);
        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneBooks([FromRoute(Name = "id")] int id)
    {

            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();
    }

    [HttpPatch("{id:int}")]

    public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
    {
        
            if (bookPatch is null)
                return BadRequest();
            var result = _manager.BookService.GetOneBookForPatch(id, false);

            var bookDto = _manager.BookService.GetOneBookById(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity();

            _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate, result.book);

            return NoContent();
        
    }

}

}


