namespace API.Helpers.Pagination
{
    public class PaginationHeader : IPagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}