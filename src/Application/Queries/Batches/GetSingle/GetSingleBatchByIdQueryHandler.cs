using Application.Abstractions.Messaging;
using Application.Queries.Batches.Common;
using Application.Queries.Batches.GetAll;
using Exceptions;
using Kogan.Mobile.Application.Common.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Batches.GetSingle
{
    public sealed class GetSingleBatchByIdQueryHandler : IQueryHandler<GetSingleBatchByIdQuery, BatchResult>
    {
        private readonly ILogger<GetAllBatchesQueryHandler> _logger;
        private readonly IKoganMobileContext _koganMobileContext;

        public GetSingleBatchByIdQueryHandler(ILogger<GetAllBatchesQueryHandler> logger, IKoganMobileContext koganMobileContext)
        {
            this._logger = logger;
            this._koganMobileContext = koganMobileContext;
        }

        public async Task<BatchResult> Handle(GetSingleBatchByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
            {
                ThrowNotFoundException(request.Id);
            }

            var batch = await this._koganMobileContext.Batches
                .Include(b => b.Vouchers)
                .ThenInclude(v => v.Pins)
                .Include(b => b.Vouchers)
                .ThenInclude(v => v.Voucher)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id);

            if (batch is null)
            {
                ThrowNotFoundException(request.Id);
            }

            return batch.Adapt<BatchResult>();
        }

        private static void ThrowNotFoundException(int id)
        {
            throw new EntityNotFoundException("Batch", id);
        }
    }
}
