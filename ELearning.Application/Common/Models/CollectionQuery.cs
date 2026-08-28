namespace ELearning.Application.Common.Models;

public sealed class CollectionQuery
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public string? Search { get; init; }
}