using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")]int id)
        {
            var book = ApplicationContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); //400 kodu
                }

                ApplicationContext.Books.Add(book);
                return StatusCode(201,book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            //check book
            var entity = ApplicationContext.Books.Find(b => b.Id.Equals(id));

            if (entity is null)
            {
                return NotFound();//404
            }

            //check şd
            if (id != book.Id)
            {
                return BadRequest();//400
            }

            ApplicationContext.Books.Remove(entity);
            book.Id = entity.Id;
            ApplicationContext.Books.Add(book);
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks(Book book)
        {
            ApplicationContext.Books.Clear();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext.Books.Find(a => a.Id.Equals(id));

            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode=404,
                    message = $"Book with id :{id} could not found"
                });
            }

            ApplicationContext.Books.Remove(entity);
            return NoContent();
        }

        [HttpPatch("{id:int}")]

        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument <Book> bookPatch)
        {
            //check
            var entity = ApplicationContext.Books.Find(e => e.Id.Equals(id));

            if (entity is null)
            {
                return NotFound();
            }

            bookPatch.ApplyTo(entity);
            return NoContent();
        }

    }
}
