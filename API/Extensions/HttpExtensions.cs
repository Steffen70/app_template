using System.Text.Json;
using API.Helpers.Pagination;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static PagedList<TList> AddPaginationHeader<TList>(this HttpResponse response, PagedList<TList> pagedList)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(pagedList.Header, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

            return pagedList;
        }
    }
}