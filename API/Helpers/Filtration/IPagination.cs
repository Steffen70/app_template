namespace API.Helpers.Filtration
{
    public interface IPagination
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
    }
}