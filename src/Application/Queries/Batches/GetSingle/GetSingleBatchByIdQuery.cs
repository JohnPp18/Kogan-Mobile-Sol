using Application.Abstractions.Messaging;
using Application.Queries.Batches.Common;

namespace Application.Queries.Batches.GetSingle
{
    public class GetSingleBatchByIdQuery : IQuery<BatchResult>
    {
        public int Id { get; init; }

        public GetSingleBatchByIdQuery(int id)
        {
            Id = id;
        }
    }
}
