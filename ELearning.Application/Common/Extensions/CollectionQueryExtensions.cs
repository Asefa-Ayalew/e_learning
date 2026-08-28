using ELearning.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Common.Extensions;

public static class CollectionQueryExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        CollectionQuery request,
        CancellationToken cancellationToken = default)
    {
        var page = request.Page < 1
            ? 1
            : request.Page;

        var pageSize = request.PageSize < 1
            ? 10
            : Math.Min(request.PageSize, 100);

        var total = await query.CountAsync(
            cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            Items = items,
            Total = total
        };
    }
}