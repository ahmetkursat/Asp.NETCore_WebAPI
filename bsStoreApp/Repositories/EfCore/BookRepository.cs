using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EfCore.Extencions;

namespace Repositories.EfCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
            

        }

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
           var books = await FindAll(trackChanges).FilterBooks(bookParameters.MinPrice,bookParameters.MaxPrice).Search(bookParameters.SearchTerm)
                .Sort(bookParameters.OrderBy).ToListAsync();

            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber,bookParameters.PageSize);
        }


        public async Task<Book> GetOneBookByIdAsync(bool trackChanges, int id) => await FindByCondition(b => b.Id.Equals(id),trackChanges).OrderBy(b => b.Id).SingleOrDefaultAsync();


        public void CreateOneBook(Book book) => Create(book);


        public void UpdateOneBook(Book book) => Update(book);


        public void DeleteOneBook(Book book) => Delete(book);

        
    }
}
