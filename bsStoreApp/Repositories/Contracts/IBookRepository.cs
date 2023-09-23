using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IBookRepository :IRepositoryBase<Book>
    {
        IQueryable<Book> GetAllBooks(bool trackChanges);
        IQueryable<Book> GetOneBookById(bool trackChanges,int id);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);


    }
}
