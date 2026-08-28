namespace ELearning.Application.Common.Models;

public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } =
        Array.Empty<T>();

    public int Total { get; init; }
}