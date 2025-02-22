using BookManagement.DAL.DTOs.Books;
using BookManagement.Models.Books;
using BookManagement.Models.Books.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Repositories;
public class BookRepository : IBookRepository
{
    private readonly AppDbContext DbContext;

    public BookRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<Guid> Add(Book book)
    {
        await DbContext.Set<Book>().AddAsync(book);
        await DbContext.SaveChangesAsync();
        return book.Id;
    }

    public async Task Add(IEnumerable<Book> books)
    {
        await DbContext.Set<Book>().AddRangeAsync(books);
        await DbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var book = await DbContext.Set<Book>().FindAsync(id);
        if (book != null)
        {
            DbContext.Set<Book>().Remove(book);
        }
    }

    public async Task Delete(IEnumerable<Guid> ids)
    {
        await DbContext.Set<Book>()
        .Where(b => ids.Contains(b.Id))
        .ExecuteUpdateAsync(b => b.SetProperty(book => book.IsDeleted, true));
    }

    public async Task<PagedList<string>> GetBookTitlesAsync(int page, int pageSize)
    {
        IQueryable<Book> booksQuery = DbContext.Set<Book>();
        booksQuery = booksQuery.OrderByDescending(b => b.ViewsCount);
        IQueryable<string> bookTitlesQuery = booksQuery.Select(b => b.Title);
        var books = await PagedList<string>.CreateAsync(bookTitlesQuery, page, pageSize);
        return books;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        var book = await DbContext.Set<Book>().FirstOrDefaultAsync(b => b.Id == id);
        if (book != null)
        {
            book.IncreaseViewsCount();
            await DbContext.Set<Book>()
            .Where(b => b.Id == book.Id)
            .ExecuteUpdateAsync(b => b.SetProperty(book => book.ViewsCount, book.ViewsCount));
        }
        return book;
    }

    public async Task Update(Book book)
    {
        var existingBook = await DbContext.Set<Book>().FindAsync(book.Id);

        if (existingBook == null)
        {
            throw new ApplicationException("Book not found.");
        }

        DbContext.Entry(existingBook).CurrentValues.SetValues(book);

        await DbContext.SaveChangesAsync();
    }
}
