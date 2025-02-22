using BookManagement.DAL.DTOs.Books;
using BookManagement.DAL.Repositories;
using BookManagement.DAL.Services;
using BookManagement.Models.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        this._bookService = bookService;
    }

    // POST api/books
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] BookPostDto book)
    {
        if (book == null)
            return BadRequest("Book data cannot be null.");

        try
        {
            var id = await _bookService.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/books/bulk
    [HttpPost("bulk")]
    public async Task<IActionResult> AddBulk([FromBody] IEnumerable<BookPostDto> books)
    {
        if (books == null || !books.Any())
            return BadRequest("No book data provided.");

        try
        {
            await _bookService.AddBulk(books);
            return Ok("Books added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _bookService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/books/bulk
    [HttpDelete("bulk")]
    public async Task<IActionResult> DeleteBulk([FromBody] IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
            return BadRequest("No IDs provided.");

        try
        {
            await _bookService.Delete(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/books/titles?page=1&pageSize=10
    [HttpGet("titles")]
    public async Task<IActionResult> GetBookTitles([FromQuery] int page, [FromQuery] int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Page and page size must be greater than zero.");

        try
        {
            var titles = await _bookService.GetBookTitlesAsync(page, pageSize);
            return Ok(titles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var bookDto = await _bookService.GetByIdAsync(id);
            if (bookDto == null)
                return NotFound($"Book with ID {id} not found.");

            return Ok(bookDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT api/books
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] BookDetailGetDto book)
    {
        if (book == null)
            return BadRequest("Book data cannot be null.");

        try
        {
            await _bookService.Update(book);
            return Ok("Book updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
