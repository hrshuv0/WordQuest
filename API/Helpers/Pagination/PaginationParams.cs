namespace API.Helpers.Pagination;

public class PaginationParams
{
    private readonly HttpRequest _request;
    private const int MaxPageSize = 100;
    
    public PaginationParams(HttpRequest request)
    {
        _request = request;
    }
    
    private int Start => Convert.ToInt32(_request.Query["start"]);
    private int Length => Convert.ToInt32(_request.Query["length"]);
    public string SearchText => _request.Query["search[value]"]!;

    public int PageNumber
    {
        get
        {
            if (Length > 0)
                return (Start / Length) + 1;
            return 1;
        }
    }

    public int PageSize => Length == 0 ? 10 : Length > MaxPageSize ? MaxPageSize : Length;


    // public int Params
    // {
    //     get => PageSize;
    //     set => PageSize = value > MaxPageSize ? MaxPageSize : value;
    // }
}