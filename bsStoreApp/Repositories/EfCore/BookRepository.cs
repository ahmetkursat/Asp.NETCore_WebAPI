using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
            

        }

        public IQueryable<Book> GetAllBooks(bool trackChanges) => FindAll(trackChanges);


        public Book GetOneBookById(bool trackChanges, int id) => FindByCondition(b => b.Id.Equals(id),trackChanges).OrderBy(b => b.Id).SingleOrDefault();


        public void CreateOneBook(Book book) => Create(book);


        public void UpdateOneBook(Book book) => Update(book);


        public void DeleteOneBook(Book book) => Delete(book);

    }
}
