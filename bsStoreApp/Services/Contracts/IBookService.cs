using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeature;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges , BookParameters bookParameters);
        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsert book);
        Task UpdateOneBookAsync(int id,BookDtoForUpdate book ,bool trackChanges);
        Task DeleteOneBookAsync(int id,bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);

    }
}
