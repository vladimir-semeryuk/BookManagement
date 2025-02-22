using BookManagement.DAL.DTOs.Books;
using BookManagement.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Services;
public interface IBookService
{
    Task<PagedList<string>> GetBookTitlesAsync(int page, int pageSize);
    Task<BookDetailGetDto?> GetByIdAsync(Guid id);
    Task<Guid> Add(BookPostDto book);
    Task AddBulk(IEnumerable<BookPostDto> books);
    Task Update(BookDetailGetDto book);
    Task Delete(Guid id);
    Task Delete(IEnumerable<Guid> ids);
}
