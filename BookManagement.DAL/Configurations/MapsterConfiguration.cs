using BookManagement.DAL.DTOs.Books;
using BookManagement.Models.Books;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Configurations;
public static class MapsterConfiguration
{
    public static void ConfigureMapster()
    {
        TypeAdapterConfig<Book, BookDetailGetDto>.NewConfig()
            .Map(dest => dest.PublicationYear, src => src.Year.Year)
            .Map(dest => dest.PublicationYearEra, src => src.Year.Era.ToString());
        TypeAdapterConfig<BookDetailGetDto, Book>.NewConfig()
            .ConstructUsing(src => Book.Create(src.Title, new PublicationYear(src.PublicationYear, Enum.Parse<Era>(src.PublicationYearEra, true)), src.AuthorName))
            .Map(dest => dest.Year, src => new PublicationYear(src.PublicationYear, Enum.Parse<Era>(src.PublicationYearEra, true)));
        TypeAdapterConfig<BookPostDto, Book>.NewConfig()
            .ConstructUsing(src => Book.Create(src.Title, new PublicationYear(src.PublicationYear, Enum.Parse<Era>(src.PublicationYearEra, true)), src.AuthorName))
            .Map(dest => dest.Id, src => Guid.NewGuid());
    }
}
