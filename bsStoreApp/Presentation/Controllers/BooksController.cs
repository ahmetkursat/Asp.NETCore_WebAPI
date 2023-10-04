using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                try
                {
                    var book = _manager.BookService.GetOneBookById(id, false);
                    if (book is null)
                    {
                        return NotFound();
                    }

                    return Ok(book);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            [HttpPost]
            public IActionResult CreateOneBooks([FromBody] Book book)
            {
                try
                {

                    if (book is null)
                    {
                        return BadRequest();
                    }

                    _manager.BookService.CreateOneBook(book);


                    return StatusCode(201, book);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            [HttpPut("{id:int}")]
            public IActionResult Update([FromRoute(Name = "id")] int id, [FromBody] Book book)
            {
                try
                {
                    if (book is null)
                    {
                        return BadRequest();
                    }
                    _manager.BookService.UpdateOneBook(id, book, true);
                    return NoContent();

                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }

            }
            [HttpDelete("{id:int}")]
            public IActionResult DeleteOneBooks([FromRoute(Name = "id")] int id)
            {
     
                try
                {
                    _manager.BookService.DeleteOneBook(id, false);
                    return NoContent();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            [HttpPatch("{id:int}")]

            public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
                [FromBody] JsonPatchDocument bookPatch)
            {
                try
                {
                    var entity = _manager.BookService.GetOneBookById(id, false);

                    if (entity is null)
                    {
                        return NotFound();
                    }

                    _manager.BookService.UpdateOneBook(id, entity, false);


                    return NoContent();
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);

                }
            }


    }

}

