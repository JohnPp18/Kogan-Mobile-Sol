﻿namespace Application.Commands.Batches.LoadBatch
{
    public class BatchCsvLine
    {
        public required int Id { get; init; }

        public required string BatchId { get; init; }

        public required string SerialNumber { get; init; }

        public required string VoucherPin { get; init; }

        public required string Name { get; init; }

        public required string Exclusory { get; init; }

        public required DateTime ValidFrom { get; init; }

        public required DateTime ValidTill { get; init; }

        public string EventType { get; init; }

        public required string State { get; init; }

        public string Msisdn { get; init; }

        /// <remarks>
        /// The retail price is provided as a null value from the CSV.
        /// </remarks>
        public decimal? RetailPrice { get; init; }
    }
}
