﻿using Infrastructure.Data;
using Kogan.Mobile.Application.Common.Interfaces;
using Kogan.Mobile.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var conString = builder.Configuration.GetConnectionString("vouchersSqlLite");

            builder.Services.AddDbContext<KoganMobileContext>((sp, opts) =>
            {
                opts.UseSqlite(conString);
            });

            builder.Services.AddScoped<IKoganMobileContext>(provider => provider.GetRequiredService<KoganMobileContext>());
            builder.Services.AddScoped<KoganMobileContextInitializer>();
            builder.Services.AddSingleton(TimeProvider.System);
        }
    }
}
