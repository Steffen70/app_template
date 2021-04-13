namespace API.Helpers.Pagination
{
    public class PaginationHeader
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        //PaginationParams
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}