using BookManagement.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Configurations;
internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(book => book.Id);

        builder.OwnsOne(book => book.Year, pubYearBuilder =>
        {
            pubYearBuilder.Property(y => y.Year)
                .IsRequired();

            pubYearBuilder.Property(y => y.Era)
                .HasConversion(
                    era => era.ToString(),
                    value => Enum.Parse<Era>(value)
                )
                .IsRequired();
        });
        builder.Property(book => book.Title).IsRequired();
        builder.Property(book => book.AuthorName).IsRequired();

        builder.HasIndex(b => b.Title).IsUnique();

        builder.HasQueryFilter(book => !book.IsDeleted);
    }
}
