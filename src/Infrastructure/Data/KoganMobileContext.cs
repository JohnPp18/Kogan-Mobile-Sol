﻿using Kogan.Domain.BusinessPartners;
using Kogan.Domain.BusinessPartners.Enums;
using Kogan.Domain.Common.Interfaces;
using Kogan.Domain.SAP.Interfaces;
using Kogan.ErpSync.IntegrationData.ValueConverters;
using Kogan.Mobile.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Mobile;
using Kogan.Mobile.Domain.Mobile.Enums;
using Kogan.Mobile.Application.Common.Interfaces;
using Kogan.Mobile.Domain.Common.Interfaces;

namespace Infrastructure.Data
{
    public class KoganMobileContext : DbContext, IKoganMobileContext
    {
        public DbSet<Voucher> Vouchers => this.Set<Voucher>();
        public DbSet<Batch> Batches => this.Set<Batch>();
        public DbSet<BusinessPartner> BusinessPartners => this.Set<BusinessPartner>();
        public DbSet<VoucherPin> VoucherPins => this.Set<VoucherPin>();
        public IQueryable<Supplier> Suppliers => this.Set<Supplier>();
        public IQueryable<Customer> Customers => this.Set<Customer>();

        public KoganMobileContext(DbContextOptions<KoganMobileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Generic Configuration - Per Interface
            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {
                if (table.ClrType.IsAssignableTo(typeof(IEntity)) && table.BaseType == null)
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

                if (table.ClrType.IsAssignableTo(typeof(IActivable)))
                {
                    modelBuilder.Entity(table.ClrType, opts =>
                    {
                        opts
                            .Property(nameof(IActivable.Active))
                            .HasDefaultValue(true);
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

                opts
                    .Property(v => v.State)
                    .HasConversion<string>(new EnumMemberToStringConverter<VoucherPinStateEnum>())
                    .HasMaxLength(4);
            });


            modelBuilder.Entity<Batch>(opts =>
            {
                opts
                    .HasIndex(b => b.SupplierBatchId)
                    .IsUnique();

                opts
                    .Property(b => b.SupplierBatchId)
                    .IsRequired()
                    .HasMaxLength(16);

                opts
                    .Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                opts
                    .Property(b => b.SupplierBatchId)
                    .ValueGeneratedNever(); // User has to specifically provide the Batch ID

                opts
                    .Property(b => b.TotalQuantity)
                    .IsRequired();

                opts
                    .Property(b => b.PlanSize)
                    .HasConversion<string>(new EnumMemberToStringConverter<MobileVoucherPlanSizeEnum>())
                    .HasMaxLength(2);

                opts
                    .Property(b => b.Country)
                    .HasConversion<string>(new EnumMemberToStringConverter<VoucherCountryEnum>())
                    .HasMaxLength(2)
                    .IsRequired();

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
                    .OnDelete(DeleteBehavior.Cascade);

                opts
                    .HasOne(bA => bA.Batch)
                    .WithMany(b => b.Vouchers)
                    .HasForeignKey(bA => bA.IdBatch)
                    .OnDelete(DeleteBehavior.Cascade);

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

                opts
                    .Property(s => s.VoucherCountry)
                    .HasConversion<string>(new EnumMemberToStringConverter<VoucherCountryEnum>())
                    .HasMaxLength(2)
                    .IsRequired()
                    .HasDefaultValue(VoucherCountryEnum.None);
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
        }
    }
}
