using BookManagement.Models.Abstractions;
using BookManagement.Models.Books.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Models.Books;
public class Book : Entity, ISoftDeletable
{
    public string Title { get; private set; }
    public PublicationYear Year { get; private set; }
    public string AuthorName { get; private set; }
    public int ViewsCount { get; private set; }
    public bool IsDeleted { get; private set; } = false;

    public decimal GetPopularityScore(IPopularityScoreService popularityScoreService)
    {
        return popularityScoreService.CalculatePopularityScore(ViewsCount, Year);
    }

    private Book() { }

    private Book(Guid id, string title, PublicationYear publicationYear, string authorName)
        : base(id)
    {
        Title = title;
        Year = publicationYear;
        ViewsCount = 0;
        AuthorName = authorName;
    }
    public static Book Create(string title, PublicationYear publicationYear, string authorName)
    {
        var book = new Book(Guid.NewGuid(), title, publicationYear, authorName);
        return book;
    }

    public void Update(string title, PublicationYear publicationYear, string authorName)
    {
        Title = title;
        Year = publicationYear;
        AuthorName = authorName;
    }

    public void IncreaseViewsCount()
    {
        ViewsCount++;
    }
}
