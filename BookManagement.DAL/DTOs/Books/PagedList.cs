using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.DTOs.Books;
public class PagedList<T>
{
    private PagedList(List<T> entities, int page, int pageSize, int totalCount)
    {
        Entities = entities;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public List<T> Entities { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        int totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new(items, page, pageSize, totalCount);
    }
}
