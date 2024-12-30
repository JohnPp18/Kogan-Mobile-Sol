using Application.Abstractions.Messaging;

namespace Application.Queries.Common
{
    public abstract class GetAllQuery<TResult> : IQuery<TResult>, IPaginated
    {
        #region Properties
        public int Page { get; private init; }

        public int PageSize { get; private init; } 
        #endregion

        protected GetAllQuery(int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            if (pageSize > 50)
            {
                pageSize = 50;
            }

            this.Page = page;
            this.PageSize = pageSize;
        }
    }
}
