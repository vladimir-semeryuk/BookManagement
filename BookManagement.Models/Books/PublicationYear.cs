using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Models.Books;
public record PublicationYear
{
    public int Year { get; }
    public Era Era { get; }
    private PublicationYear() { }
    public PublicationYear(int year, Era? era = Era.AD)
    {
        if (year <= 0)
            throw new ArgumentOutOfRangeException(nameof(year), "Year must be greater than zero.");

        Year = year;
        Era = era ?? Era.AD;
    }
};
