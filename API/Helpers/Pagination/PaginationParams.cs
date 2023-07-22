namespace API.Helpers.Pagination;

public class PaginationParams
{
    private const int MaxPageSize = 50;
    
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int Params
    {
        get => PageSize;
        set => PageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}