namespace BookManagement.DAL.DTOs.Books;

public class BookPostDto
{
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public int PublicationYear { get; set; }
    public string PublicationYearEra { get; set; }
}