using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
            

        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges) => await FindAll(trackChanges).Skip((bookParameters.PageNumber-1) * bookParameters.PageSize).Take(bookParameters.PageSize).OrderBy(b => b.Id).ToListAsync();


        public async Task<Book> GetOneBookByIdAsync(bool trackChanges, int id) => await FindByCondition(b => b.Id.Equals(id),trackChanges).OrderBy(b => b.Id).SingleOrDefaultAsync();


        public void CreateOneBook(Book book) => Create(book);


        public void UpdateOneBook(Book book) => Update(book);


        public void DeleteOneBook(Book book) => Delete(book);

        
    }
}
