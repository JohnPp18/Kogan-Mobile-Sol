using Application.Abstractions.Messaging;
using Application.Extensions;
using Application.Queries.Batches.Common;
using Kogan.Mobile.Application.Common.Interfaces;
using Kogan.Mobile.Domain.Mobile;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Batches.GetAll
{
    public sealed class GetAllBatchesQueryHandler : IQueryHandler<GetAllBatchesQuery, GetAllBatchResult>
    {
        private readonly ILogger<GetAllBatchesQueryHandler> _logger;
        private readonly IKoganMobileContext _koganMobileContext;

        public GetAllBatchesQueryHandler(ILogger<GetAllBatchesQueryHandler> logger, IKoganMobileContext koganMobileContext)
        {
            this._logger = logger;
            this._koganMobileContext = koganMobileContext;
        }

        public async Task<GetAllBatchResult> Handle(GetAllBatchesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Batch> queryBatch = this._koganMobileContext
                .Batches
                .Include(b => b.Supplier)
                .Include(b => b.Vouchers)
                .AsNoTracking()
                .OrderByDescending(b => b.Id);

            int totalItems = await queryBatch.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            var results = await queryBatch
                .SkipPreviousPage(request.Page, request.PageSize)
                .Select(b => b.Adapt<BatchResult>())
                .ToListAsync();

            return new GetAllBatchResult()
            {²
                CurrentPage = request.Page,
                CurrentPageItemCount = results.Count,
                Items = results,
                PageSize = request.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems/request.PageSize)
            };
        }
    }
}
