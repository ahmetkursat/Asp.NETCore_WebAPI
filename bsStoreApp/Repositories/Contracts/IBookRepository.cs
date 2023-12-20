using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestParameters;

namespace Repositories.Contracts
{
    public interface IBookRepository :IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters ,bool trackChanges);
        Task<Book> GetOneBookByIdAsync(bool trackChanges,int id);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);

    }
}
