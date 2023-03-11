namespace CleanArchitecture.Application.Features.Shared.Queries;

public class PaginationVm<T> where T : class
{
    public int Count { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public IReadOnlyCollection<T>? Data { get; set; }
}
