using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.DTOs.Books;
public class BookDetailGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public int PublicationYear { get; set; }
    public string PublicationYearEra { get; set; }
    public decimal PopularityScore { get; set; }
}
