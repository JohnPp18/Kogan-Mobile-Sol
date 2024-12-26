using Kogan.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Mobile;
using Microsoft.EntityFrameworkCore;

namespace Kogan.Mobile.Infrastructure.Data
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
