using Kogan.Domain.BusinessPartners;
using Kogan.Mobile.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Mobile;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kogan.Mobile.Application.Common.Interfaces
{
    public interface IKoganMobileContext
    {
        DbSet<Voucher> Vouchers { get; }

        DbSet<VoucherPin> VoucherPins { get; }

        DbSet<Batch> Batches { get; }

        DbSet<BusinessPartner> BusinessPartners { get; }

        DbSet<Supplier> Suppliers { get; }

        DbSet<Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
         where TEntity : class;
    }
}
