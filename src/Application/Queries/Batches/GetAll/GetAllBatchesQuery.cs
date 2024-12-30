using Application.Queries.Common;

namespace Application.Queries.Batches.GetAll
{
    public class GetAllBatchesQuery : GetAllQuery<GetAllBatchResult>
    {

        public GetAllBatchesQuery(int page, int pageSize)
            :base(page, pageSize)
        {

        }
    }
}
