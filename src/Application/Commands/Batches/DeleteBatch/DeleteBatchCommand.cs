using Exceptions;
using Kogan.Mobile.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Batches.DeleteBatch
{
    public record DeleteBatchCommand : IRequest
    {
        public int Id { get; private set; }

        public DeleteBatchCommand(int id)
        {
            this.Id = id;
        }
    }

    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand>
    {
        private IKoganMobileContext _koganMobileContext;
        private ILogger<DeleteBatchCommandHandler> _logger;

        public DeleteBatchCommandHandler(IKoganMobileContext koganMobileContext, ILogger<DeleteBatchCommandHandler> logger)
        {
            this._koganMobileContext = koganMobileContext;
            this._logger = logger;
        }

        public async Task Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            var batchData = await this._koganMobileContext.Batches
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id);

            if (batchData is null)
            {
                throw new BatchNotFoundException(request.Id);
            }

            if (!string.IsNullOrWhiteSpace(batchData.ObjectKey))
            {
                throw new CannotDeleteBatchException(request.Id, $"Batch has already been synced to SAP (ObjectType:{batchData.ObjectType}, ObjectKey={batchData.ObjectKey}).");
            }

            // Ok to delete
            this._koganMobileContext.Batches.Remove(batchData);

            await this._koganMobileContext.SaveChangesAsync(cancellationToken);
        }
    }
}
