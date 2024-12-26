using CsvHelper.Configuration;

namespace Kogan.Mobile.Application.Batches.Commands.LoadBatch
{
    public class BatchCsvLineClassMap : ClassMap<BatchCsvLine>
    {
        public BatchCsvLineClassMap()
        {
            this.Map(b => b.Id)
                .Name("ID");

            this.Map(b => b.BatchId)
                .Name("BATCH ID");

            this.Map(b => b.SerialNumber)
                .Name("SERIAL_NUMBER");

            this.Map(b => b.VoucherPin)
                .Name("VOUCHER PIN");

            this.Map(b => b.Name)
                .Name("NAME");

            this.Map(b => b.Exclusory)
                .Name("EXCLUSORY");

            this.Map(b => b.ValidFrom)
                .Name("VALID FROM");

            this.Map(b => b.ValidTill)
                .Name("VALID TILL");

            this.Map(b => b.EventType)
                .Name("EVENT TYPE");

            this.Map(b => b.State)
                .Name("STATE");

            this.Map(b => b.Msisdn)
                .Name("MSISDN");

            this.Map(b => b.RetailPrice)
                .Name("RETAIL PRICE");
        }
    }
}
