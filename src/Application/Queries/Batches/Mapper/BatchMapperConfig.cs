using Kogan.Mobile.Domain.Mobile;
using Kogan.Mobile.Domain.Mobile.Enums;
using Mapster;
using static Application.Queries.Batches.Common.BatchResult;

namespace Application.Queries.Batches.Mapper
{
    public sealed class BatchMapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .NewConfig<BatchVoucherAssociation, BatchVoucherResult>()
                .MapWith((db) => MapperFunc(db));
        }

        private static BatchVoucherResult MapperFunc(BatchVoucherAssociation bVA)
        {
            return new BatchVoucherResult()
            {
                Name = bVA.Voucher?.Name,
                SimType = bVA.Voucher?.SimType ?? MobileVoucherSimTypeEnum.None,
                WebSku = bVA.Voucher?.WebSku,
                Pins = bVA.Pins?.Select(p => p.Adapt<VoucherPinResult>())
            };
        }
    }
}
