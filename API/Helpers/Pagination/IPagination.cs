namespace API.Helpers.Pagination
{
    public interface IPagination
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
    }
}