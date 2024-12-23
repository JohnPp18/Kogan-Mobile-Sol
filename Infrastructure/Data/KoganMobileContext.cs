using Kogan.Domain.BusinessPartners;
using Kogan.Domain.BusinessPartners.Enums;
using Kogan.Domain.Common.Interfaces;
using Kogan.Domain.SAP.Interfaces;
using Kogan.ErpSync.IntegrationData.ValueConverters;
using Kogan.Mobile.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Mobile.Enums;
using Kogan.Mobile.IntegrationData.Mobile;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class KoganMobileContext : DbContext
    {
        public DbSet<Voucher> Vouchers => this.Set<Voucher>();
        public DbSet<Batch> Batches => this.Set<Batch>();
        public DbSet<BusinessPartner> BusinessPartners => this.Set<BusinessPartner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Generic Configuration - Per Interface
            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {
                if (table.ClrType.IsAssignableTo(typeof(IEntity)))
                {
                    modelBuilder.Entity(table.ClrType, opts =>
                    {
                        opts
                            .HasKey(nameof(IEntity.Id));

                        opts
                            .Property(nameof(IEntity.Id))
                            .HasColumnOrder(0);
                    });
                }

                if (table.ClrType.IsAssignableTo(typeof(IDescriptive)))
                {
                    modelBuilder.Entity(table.ClrType, opts =>
                    {
                        opts
                            .Property(nameof(IDescriptive.Description))
                            .HasMaxLength(254);
                    });
                }

                if (table.ClrType.IsAssignableTo(typeof(ICreatable)))
                {
                    modelBuilder.Entity(table.ClrType, opts =>
                    {
                        opts
                            .Property(nameof(ICreatable.CreatedAtUtc))
                            .IsRequired()
                            .Metadata
                            .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
                    });
                }

                if (table.ClrType.IsAssignableTo(typeof(IArticle)))
                {
                    modelBuilder.Entity(table.ClrType, opts =>
                    {
                        opts
                            .Property(nameof(IArticle.Name))
                            .HasMaxLength(100);

                        opts
                            .Property(nameof(IArticle.WebSku))
                            .HasMaxLength(100);

                        opts
                            .HasIndex(nameof(IArticle.WebSku))
                            .IsUnique(true);
                    });
                }

                if (table.ClrType.IsAssignableTo(typeof(ISapSynchronizable)))
                {
                    modelBuilder.Entity(table.ClrType, opts =>
                    {
                        opts
                            .Property(nameof(ISapSynchronizable.ObjectKey))
                            .HasMaxLength(16)
                            .HasColumnOrder(999);

                        opts
                            .Property(nameof(ISapSynchronizable.ObjectType))
                            .HasMaxLength(20) // In SAP, a UDO code can have up to 20 characters which would be the limit
                            .HasColumnOrder(1000);

                        opts
                            .HasIndex(nameof(ISapSynchronizable.ObjectType), nameof(ISapSynchronizable.ObjectKey))
                            .IsUnique(true);
                    });
                }
            }

            // Specific configuration - Per Table
            modelBuilder.Entity<Voucher>(opts =>
            {
                opts
                    .Property(v => v.SimType)
                    .HasConversion<string>(new EnumMemberToStringConverter<MobileVoucherSimTypeEnum>())
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<VoucherPin>(opts =>
            {
                opts.HasKey(vP => new
                {
                    vP.IdBatch,
                    vP.IdVoucher,
                    vP.PinNumber
                });

                opts
                    .Property(vP => vP.Msisdn)
                    .HasMaxLength(15);

                opts
                    .Property(vP => vP.PinNumber)
                    .HasMaxLength(20)
                    .IsRequired(true);

                opts
                    .Property(vP => vP.IsRedeemed)
                    .HasDefaultValue(value: false);

                opts
                    .Property(vP => vP.IsSold)
                    .HasDefaultValue(value: false);

                opts
                    .Property(vP => vP.IsExpired)
                    .HasDefaultValue(value: false);
            });


            modelBuilder.Entity<Batch>(opts =>
            {
                opts
                    .HasIndex(mB => mB.SupplierBatchId)
                    .IsUnique();

                opts
                    .Property(mB => mB.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                opts
                    .Property(mB => mB.SupplierBatchId)
                    .ValueGeneratedNever(); // User has to specifically provide the Batch ID

                opts
                    .Property(mB => mB.TotalQuantity)
                    .IsRequired();

                opts
                    .Property(v => v.PlanSize)
                    .HasConversion<string>(new EnumMemberToStringConverter<MobileVoucherPlanSizeEnum>())
                    .HasMaxLength(2);

                opts
                    .HasOne(b => b.Supplier)
                    .WithMany()
                    .HasForeignKey(b => b.IdSupplier)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BatchVoucherAssociation>(opts =>
            {
                opts
                    .HasKey(mBA => new
                    {
                        mBA.IdBatch,
                        mBA.IdVoucher
                    });

                opts
                    .HasOne(bA => bA.Voucher)
                    .WithMany(v => v.Batches)
                    .HasForeignKey(bA => bA.IdVoucher)
                    .OnDelete(DeleteBehavior.Restrict);

                opts
                    .HasOne(bA => bA.Batch)
                    .WithMany(b => b.Vouchers)
                    .HasForeignKey(bA => bA.IdBatch)
                    .OnDelete(DeleteBehavior.Restrict);

                opts
                    .HasMany(bA => bA.Pins)
                    .WithOne(p => p.BatchVoucherAssociation)
                    .HasForeignKey(p => new
                    {
                        p.IdBatch,
                        p.IdVoucher
                    })
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Supplier>(opts =>
            {
                opts
                    .Property(s => s.Name)
                    .HasMaxLength(100)
                    .IsRequired(true);

                opts
                    .Property(s => s.DefComPercent)
                    .IsRequired(true)
                    .HasDefaultValue(86);
            });

            // Add basic data
            modelBuilder.Entity<BusinessPartner>(opts =>
            {
                opts.HasDiscriminator<BusinessPartnerTypeEnum>(bP => bP.Type)
                .HasValue<Supplier>(BusinessPartnerTypeEnum.Supplier)
                .HasValue<Customer>(BusinessPartnerTypeEnum.Customer);

                opts.Property(bP => bP.Type)
                .HasConversion<string>(new EnumMemberToStringConverter<BusinessPartnerTypeEnum>())
                .HasMaxLength(1);
            });

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier()
                {
                    Id = 1,
                    ObjectType= "2",
                    ObjectKey = "S0000000",
                    Name = "Vodafone Australia",
                    DefComPercent = 86
                },
                new Supplier()
                {
                    Id = 2,
                    ObjectType = "2",
                    ObjectKey = "S0000000",
                    Name = "Vodafone New Zealand",
                    DefComPercent = 86
                }
            );
        }
    }
}
