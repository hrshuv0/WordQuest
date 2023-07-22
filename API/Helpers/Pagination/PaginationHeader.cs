namespace API.Helpers.Pagination;

public class PaginationHeader
{
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalFiltered { get; set; }
    public int TotalPages { get; set; }

    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalFiltered, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalFiltered = totalFiltered;
        TotalPages = totalPages;
    }
}