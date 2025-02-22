using BookManagement.DAL.DTOs.Books;
using BookManagement.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Repositories;
public interface IBookRepository
{
    Task<PagedList<string>> GetBookTitlesAsync(int page, int pageSize);
    Task<Book?> GetByIdAsync(Guid id);
    Task<Guid> Add(Book book);
    Task Add(IEnumerable<Book> books);
    Task Update(Book book);
    Task Delete(Guid id);
    Task Delete(IEnumerable<Guid> ids);
}
