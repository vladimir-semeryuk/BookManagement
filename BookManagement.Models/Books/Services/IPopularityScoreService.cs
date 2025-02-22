using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Models.Books.Services;
public interface IPopularityScoreService
{
    decimal CalculatePopularityScore(int bookViews, PublicationYear year);
}
