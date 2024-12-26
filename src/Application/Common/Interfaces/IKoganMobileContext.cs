using Kogan.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Mobile;

namespace Kogan.Mobile.Application.Common.Interfaces
{
    public interface IKoganMobileContext
    {
        DbSet<Voucher> Vouchers { get; }

        DbSet<VoucherPin> VoucherPins { get; }

        DbSet<Batch> Batches { get; }

        DbSet<BusinessPartner> BusinessPartners { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
