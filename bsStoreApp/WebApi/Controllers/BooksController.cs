using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.EfCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name="id")] int id)
        {
            
            try
            {
                var book = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
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
        public IActionResult GetOneBooks([FromBody] Book book)
        {
            try
            {
                
                if (book is null)
                {
                    return BadRequest();
                }

                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id , [FromBody] Book book)
        {
            try
            {
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
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

                _context.SaveChanges();

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
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                if (entity is null)
                {
                    return NotFound(new
                    {
                        statuscode = 404,
                        message = $"Book with id :{id} could not found"
                    });
                }

                _context.Books.Remove(entity);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
