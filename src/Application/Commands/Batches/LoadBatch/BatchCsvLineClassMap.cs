using CsvHelper.Configuration;

namespace Application.Commands.Batches.LoadBatch
{
    public class BatchCsvLineClassMap : ClassMap<BatchCsvLine>
    {
        public BatchCsvLineClassMap()
        {
            Map(b => b.Id)
                .Name("ID");

            Map(b => b.BatchId)
                .Name("BATCH ID");

            Map(b => b.SerialNumber)
                .Name("SERIAL_NUMBER");

            Map(b => b.VoucherPin)
                .Name("VOUCHER PIN");

            Map(b => b.Name)
                .Name("NAME");

            Map(b => b.Exclusory)
                .Name("EXCLUSORY");

            Map(b => b.ValidFrom)
                .Name("VALID FROM");

            Map(b => b.ValidTill)
                .Name("VALID TILL");

            Map(b => b.EventType)
                .Name("EVENT TYPE");

            Map(b => b.State)
                .Name("STATE");

            Map(b => b.Msisdn)
                .Name("MSISDN");

            Map(b => b.RetailPrice)
                .Name("RETAIL PRICE");
        }
    }
}
