using BookManagement.DAL.DTOs.Books;
using BookManagement.DAL.Repositories;
using BookManagement.Models.Books;
using BookManagement.Models.Books.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Services;
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IPopularityScoreService _popularityScoreService;

    public BookService(IBookRepository bookRepository, IPopularityScoreService popularityScoreService)
    {
        _bookRepository = bookRepository;
        _popularityScoreService = popularityScoreService;
    }

    public async Task<Guid> Add(BookPostDto book)
    {
        var bookToAdd = book.Adapt<Book>();
        var id = await _bookRepository.Add(bookToAdd);
        return id;
    }

    public async Task AddBulk(IEnumerable<BookPostDto> books)
    {
        var booksToAdd = books.Adapt<IEnumerable<Book>>();
        await _bookRepository.Add(booksToAdd);
    }

    public async Task Delete(Guid id)
    {
        await _bookRepository.Delete(id);
    }

    public async Task Delete(IEnumerable<Guid> ids)
    {
        await _bookRepository.Delete(ids);
    }

    public async Task<PagedList<string>> GetBookTitlesAsync(int page, int pageSize)
    {
        return await _bookRepository.GetBookTitlesAsync(page, pageSize);
    }

    public async Task<BookDetailGetDto?> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
            return null;
        var popularityScore = _popularityScoreService.CalculatePopularityScore(book.ViewsCount, book.Year);
        var result = book.Adapt<BookDetailGetDto>();
        result.PopularityScore = popularityScore;
        return result;
    }

    public async Task Update(BookDetailGetDto book)
    {
        var bookToUpdate = await _bookRepository.GetByIdAsync(book.Id);
        if (bookToUpdate == null)
            throw new ApplicationException($"The book with id {book.Id} was not found");
        bookToUpdate.Update(
            book.Title,
            new PublicationYear(book.PublicationYear, Enum.Parse<Era>(book.PublicationYearEra, true)),
            book.AuthorName);
        await _bookRepository.Update(bookToUpdate);
    }
}
