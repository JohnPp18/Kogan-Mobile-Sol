namespace Application.Queries.Common
{
    public abstract class PaginatedResponse<T> : IPaginatedResponse<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageItemCount { get; set; }
        public List<T> Items { get; set; }
    }
}
