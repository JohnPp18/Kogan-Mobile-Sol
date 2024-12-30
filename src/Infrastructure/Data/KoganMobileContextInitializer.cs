using Infrastructure.Data;
using Kogan.Mobile.Domain.BusinessPartners;
using Kogan.Mobile.Domain.Mobile.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kogan.Mobile.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<KoganMobileContextInitializer>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }

    }
    public sealed class KoganMobileContextInitializer
    {
        private readonly ILogger<KoganMobileContextInitializer> _logger;
        private readonly KoganMobileContext _context;

        public KoganMobileContextInitializer(ILogger<KoganMobileContextInitializer> logger, KoganMobileContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            bool saveChanges = false;

            if (!this._context.BusinessPartners.Any())
            {
                this._context.BusinessPartners.AddRange(
                    new Supplier()
                    {
                        Id = 1,
                        ObjectType = "2",
                        ObjectKey = "S0000000",
                        Name = "Vodafone Australia",
                        DefComPercent = 86,
                        VoucherCountry = VoucherCountryEnum.Australia,
                        Active = true
                    },
                    new Supplier()
                    {
                        Id = 2,
                        ObjectType = "2",
                        ObjectKey = "S0000001",
                        Name = "Vodafone New Zealand",
                        DefComPercent = 86,
                        VoucherCountry = VoucherCountryEnum.NewZealand,
                        Active = true
                    });

                saveChanges = true;
            }

            if (saveChanges)
            {
                await this._context.SaveChangesAsync();
            }
        }
    }
}
