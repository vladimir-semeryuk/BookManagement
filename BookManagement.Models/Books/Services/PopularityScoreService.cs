using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Models.Books.Services;
public class PopularityScoreService : IPopularityScoreService
{
    public decimal CalculatePopularityScore(int bookViews, PublicationYear year)
    {
        int currentYear = DateTime.UtcNow.Year;
        int yearsSincePublished = year.Era == Era.AD
            ? currentYear - year.Year
            : currentYear + (year.Year - 1);
        return (bookViews * 0.5m) + (yearsSincePublished * 2);
    }
}
