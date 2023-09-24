using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EfCore;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.Book.GetAllBooks(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name="id")] int id)
        {
            
            try
            {
                var book = _manager.Book.GetOneBookById(false,id);
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

                _manager.Book.CreateOneBook(book);
                _manager.Save();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute(Name = "id")] int id , [FromBody] Book book)
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(true,id);
                if (entity is null)
                {
                    return NotFound();
                }

                if (id != book.Id)
                {
                    return BadRequest();
                }

                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();

                return Ok(book);

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
                var entity = _manager.Book.GetOneBookById(false,id);

                if (entity is null)
                {
                    return NotFound(new
                    {
                        statuscode = 404,
                        message = $"Book with id :{id} could not found"
                    });
                }
                _manager.Book.DeleteOneBook(entity);
                _manager.Save();
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
                var entity = _manager.Book.GetOneBookById(true, id);

                if (entity is null)
                {
                    return NotFound();
                }
                
                _manager.Book.UpdateOneBook(entity);
                _manager.Save();

                return NoContent();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }


    }
}
