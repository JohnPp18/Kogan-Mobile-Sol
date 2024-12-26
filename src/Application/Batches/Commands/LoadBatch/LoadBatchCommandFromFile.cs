using Kogan.Mobile.Infrastructure.Data;
using MediatR;

namespace Kogan.Mobile.Application.Batches.Commands.LoadBatch
{
    public record LoadBatchCommandFromFile : IRequest<int>
    {
        public string FilePath { get; init; }
    }

    public class LoadBatchCommandFromFileCommandHandler : IRequestHandler<LoadBatchCommandFromFile, int>
    {
        private IKoganMobileContext _koganMobileContext;

        public LoadBatchCommandFromFileCommandHandler(IKoganMobileContext koganMobileContext)
        {
            ArgumentNullException.ThrowIfNull(nameof(koganMobileContext));

            this._koganMobileContext = koganMobileContext;
        }

        public Task<int> Handle(LoadBatchCommandFromFile request, CancellationToken cancellationToken)
        {

        }
    }
}
