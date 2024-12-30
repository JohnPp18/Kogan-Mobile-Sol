namespace Application.Queries.Common
{
    public interface IPaginatedQuery
    {
        int Page { get; set; }

        int PageSize { get; set; }
    }
}
