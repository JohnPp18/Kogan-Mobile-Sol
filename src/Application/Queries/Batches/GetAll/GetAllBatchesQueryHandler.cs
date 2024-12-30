using Application.Abstractions.Messaging;
using Application.Extensions;
using Kogan.Mobile.Application.Common.Interfaces;
using Kogan.Mobile.Domain.Mobile;
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
                .AsNoTracking()
                .OrderByDescending(b => b.Id);

            int totalItems = await queryBatch.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            var results = await queryBatch
                .SkipPreviousPage(request.Page, request.PageSize)
                .Select(b => new BatchResult()
                {
                    Active = b.Active,
                    Country = b.Country,
                    Description = b.Description,
                    Id = b.Id,
                    IdSupplier = b.IdSupplier,
                    Name = b.Name,
                    ObjectKey = b.ObjectKey,
                    ObjectType = b.ObjectType,
                    PlanDurationDays = b.PlanDurationDays,
                    PlanSize = b.PlanSize,
                    RedemptionDateEnd = b.RedemptionDateEnd,
                    SalesPrice = b.SalesPrice,
                    SupplierBatchId = b.SupplierBatchId,
                    SupplierComPrcnt = b.SupplierComPrcnt,
                    SupplierName = b.Supplier.Name,
                    TotalQuantity = b.TotalQuantity,
                    ValidFrom = b.ValidFrom,
                    ValidTo = b.ValidTo
                })
                .ToListAsync();

            return new GetAllBatchResult()
            {
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
