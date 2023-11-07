namespace QuizHub.Infrastructure.Data.Services;

using Microsoft.EntityFrameworkCore;

using QuizHub.Application.Common.Models;

public static class PaginationExtensions
{
    public static async Task<PaginatedList<T>> GetPaginatedListAsync<T>(
        this IQueryable<T> query,
        int perPage,
        int page
    )
    {
        return new PaginatedList<T>()
        {
            Count = await query.CountAsync(),
            Items = await query.Skip(perPage * (page - 1)).Take(perPage).ToListAsync()
        };
    }
}
