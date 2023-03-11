namespace CleanArchitecture.Application.Features.Shared.Queries;

public class PaginationBaseQuery
{
    private int _pageSize = 3;
    private int _maxPageSize = 50;

    public string? Search { get; set; }
    public string? Sort { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { 
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : _pageSize;
    }
}


