namespace Application.Queries.Common
{
    public interface IPaginatedResponse<T>
    {
        int TotalItems { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        int CurrentPageItemCount { get; set; }
        List<T> Items { get; set; }
    }
}
