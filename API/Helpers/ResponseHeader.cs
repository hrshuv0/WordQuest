using API.Helpers.Pagination;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Helpers;

public static class ResponseHeader
{
    public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems,
        int totalFiltered, int totalPages)
    {
        var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalFiltered, totalPages);
        var camelCaseFormatter = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}