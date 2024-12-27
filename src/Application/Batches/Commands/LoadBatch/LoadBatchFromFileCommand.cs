using CsvHelper;
using Exceptions;
using Kogan.Mobile.Application.Common.Interfaces;
using Kogan.Mobile.Domain.Mobile;
using Kogan.Mobile.Domain.Mobile.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Kogan.Mobile.Application.Batches.Commands.LoadBatch
{
    public record LoadBatchFromFileCommand : IRequest<int>
    {
        public sealed class SimDistributionDefinition()
        {
            public MobileVoucherSimTypeEnum SimType { get; init; }

            public int TotalQuantity { get; init; }

            public string WebSku { get; init; }
        }

        public string FilePath { get; init; }

        public int PlanDurationDays { get; init; }

        public VoucherCountryEnum Country { get; init; }

        public MobileVoucherSimTypeEnum SimType { get; init; }

        public MobileVoucherPlanSizeEnum PlanSize { get; init; }

        /// <summary>
        /// The supplier commission percent. When none provided, fallback to the default one set for this supplier.
        /// </summary>
        public decimal? SupplierComPercent { get; init; }

        public IEnumerable<SimDistributionDefinition> SimDistribution { get; init; }
    }

    public class LoadBatchCommandFromFileCommandHandler : IRequestHandler<LoadBatchFromFileCommand, int>
    {
        private readonly IKoganMobileContext _koganMobileContext;
        private readonly ILogger<LoadBatchCommandFromFileCommandHandler> _logger;
        private readonly TimeProvider _timeProvider;

        public LoadBatchCommandFromFileCommandHandler(IKoganMobileContext koganMobileContext, ILogger<LoadBatchCommandFromFileCommandHandler> logger, TimeProvider timeProvider)
        {
            this._koganMobileContext = koganMobileContext;
            this._logger = logger;
            this._timeProvider = timeProvider;
        }

        public async Task<int> Handle(LoadBatchFromFileCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request?.FilePath))
            {
                throw new BatchFileException(
                    $"Cannot load the batch CSV file.", 
                    new ArgumentNullException($"{nameof(LoadBatchFromFileCommand)}.{nameof(LoadBatchFromFileCommand.FilePath)}"));
            }

            this._logger.LogInformation($"Loading CSV batch file '{request.FilePath}' into the database.");

            using (var reader = new StreamReader(request.FilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                IEnumerable<BatchCsvLine> csvRecords = csv.GetRecords<BatchCsvLine>();

                // A batch file should contain one batch number only
                IEnumerable<string> distinctBatchNums = csvRecords
                    .Select(r => r.BatchId)
                    .Distinct();

                int numOfDistinctBatchNumbers = distinctBatchNums.Count(); 

                if (numOfDistinctBatchNumbers > 1)
                {
                    throw new BatchFileException($"A batch file should contain only one batch number. {numOfDistinctBatchNumbers} batch numbers provided: {string.Join(",", distinctBatchNums)}.");
                }

                string batchId = distinctBatchNums.First();

                // Check if this batch already exists in DB
                bool batchAlreadyExists = this._koganMobileContext
                    .Batches
                    .Any(b => b.SupplierBatchId == batchId);

                if (batchAlreadyExists)
                {
                    throw new BatchFileException($"Batch `{batchId}` was already loaded into the database.");
                }

                // Check if the sim distribution matches the total quantity
                int totalSimDistribution = request.SimDistribution.Sum(d => d.TotalQuantity);
                int totalCsvRecords = csvRecords.Count();

                if (totalCsvRecords != totalSimDistribution)
                {
                    throw new BatchFileException($"The sim distribution total quantities ({totalCsvRecords}) doesn't match the total quantity of vouchers in this CSV ({totalCsvRecords}).");
                }

                var voucherSupplier = await this._koganMobileContext.Suppliers
                    .AsNoTracking()
                    .Where(s => s.Active && s.VoucherCountry == request.Country)
                    .Select(s => new
                    {
                        s.Id,
                        s.DefComPercent
                    })
                    .SingleAsync(); // There can be only one active voucher provider per country

                // Transform the data into its DB persisted shape
                var batch = new Batch()
                {
                    Country = request.Country,
                    Name = csvRecords.First().Name,
                    ValidFrom = csvRecords.First().ValidFrom,
                    ValidTo = csvRecords.First().ValidTill,
                    IdSupplier = voucherSupplier.Id,
                    TotalQuantity = csvRecords.Count(),
                    PlanSize = MobileVoucherPlanSizeEnum.None,
                    SupplierComPrcnt = request.SupplierComPercent ?? voucherSupplier.DefComPercent,   
                    PlanDurationDays = request.PlanDurationDays
                };

                int skip = 0;
                DateTime today = this._timeProvider.GetLocalNow().Date;
                foreach (var simTypeDistribution in request.SimDistribution)
                {
                    var batchVoucherAssociation = new BatchVoucherAssociation()
                    {
                        Voucher = new Voucher()
                        {
                            WebSku = simTypeDistribution.WebSku,
                            SimType = simTypeDistribution.SimType,
                        }
                    };

                    for (int i = skip; i < simTypeDistribution.TotalQuantity; i++)
                    {
                        var csvRecord = csvRecords.ElementAt(i);

                        batchVoucherAssociation.Pins.Add(new VoucherPin()
                        {
                            Msisdn = csvRecord.Msisdn,
                            PinNumber = csvRecord.VoucherPin,
                            State = csvRecord.State == "A" ? VoucherPinStateEnum.A : VoucherPinStateEnum.None,
                            IsExpired = today > csvRecord.ValidTill,
                            IsSold = false,
                            IsRedeemed = false,
                        });
                    }

                    batch.Vouchers.Add(batchVoucherAssociation);

                    skip += simTypeDistribution.TotalQuantity;
                }

                this._koganMobileContext.Batches.Add(batch);

                await this._koganMobileContext.SaveChangesAsync(cancellationToken);

                return batch.Id;
            }
        }
    }
}
