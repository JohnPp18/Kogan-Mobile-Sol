using CsvHelper;
using Exceptions;
using Kogan.Mobile.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Kogan.Mobile.Application.Batches.Commands.LoadBatch
{
    public record LoadBatchCommandFromFile : IRequest<int>
    {
        public string FilePath { get; init; }
    }

    public class LoadBatchCommandFromFileCommandHandler : IRequestHandler<LoadBatchCommandFromFile, int>
    {
        private readonly IKoganMobileContext _koganMobileContext;
        private readonly ILogger<LoadBatchCommandFromFileCommandHandler> _logger;


        public LoadBatchCommandFromFileCommandHandler(IKoganMobileContext koganMobileContext, ILogger<LoadBatchCommandFromFileCommandHandler> logger)
        {
            this._koganMobileContext = koganMobileContext;
            this._logger = logger;
        }

        public Task<int> Handle(LoadBatchCommandFromFile request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request?.FilePath))
            {
                throw new BatchFileException(
                    $"Cannot load the batch CSV file.", 
                    new ArgumentNullException($"{nameof(LoadBatchCommandFromFile)}.{nameof(LoadBatchCommandFromFile.FilePath)}"));
            }

            this._logger.LogInformation($"Loading CSV batch file '{request.FilePath}' into the database.");

            using (var reader = new StreamReader(request.FilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                IEnumerable<BatchCsvLine> csvRecords = csv.GetRecords<BatchCsvLine>();

                // A batch file should contain one batch number only
                IEnumerable<string> distinctBatchNumbers = csvRecords
                    .Select(r => r.BatchId)
                    .Distinct();

                if (distinctBatchNumbers.Skip(1).Any())
                {
                    throw new BatchFileException($"Multiple batch numbers found in file '{request.FilePath}': {string.Join(",", distinctBatchNumbers)}");
                }

                string batchNum = distinctBatchNumbers.First();

                // Transform the payload

            }

            return new Task<int>(() => 1);
        }
    }
}
