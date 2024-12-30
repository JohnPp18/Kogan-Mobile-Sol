namespace Application.Queries.Common
{
    public interface IPaginated
    {
        int Page { get; }

        int PageSize { get; }
    }
}
