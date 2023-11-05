using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    //12.36da kaldımlogfilters
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public async Task <IActionResult> GetAllBooksAsync([FromQuery]BookParameters bookParameters)
        {
            var books = await _manager.BookService.GetAllBooksAsync(false,bookParameters);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task <IActionResult> GetOneBooksAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

            return Ok(book);
        }


        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBooksAsync([FromBody] BookDtoForInsert bookDto)
         {

       

        var book = await _manager.BookService.CreateOneBookAsync(bookDto);


        return StatusCode(201, book);

         }
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        [HttpPut("{id:int}")]
        public async Task <IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
         {

        if (bookDto is null)
        {
            return BadRequest();//400
        }

        if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);//404
            }

        await _manager.BookService.UpdateOneBookAsync(id, bookDto , false);
        return NoContent();

         }

          [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBooksAsync([FromRoute(Name = "id")] int id)
         {

            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();
          }

            [HttpPatch("{id:int}")]

        public async Task<IActionResult> PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
             [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
            {
        
            if (bookPatch is null)
                return BadRequest();
            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity();

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); //204
        
    }

        

    }

}


