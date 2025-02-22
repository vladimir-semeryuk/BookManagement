using BookManagement.DAL.Configurations;
using BookManagement.DAL.Repositories;
using BookManagement.DAL.Services;
using BookManagement.Models.Books.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                 throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, b=> b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


        services.AddScoped<IBookRepository, BookRepository>();
        services.AddTransient<IPopularityScoreService, PopularityScoreService>();
        services.AddScoped<IBookService, BookService>();

        MapsterConfiguration.ConfigureMapster();

        return services;
    }
}
