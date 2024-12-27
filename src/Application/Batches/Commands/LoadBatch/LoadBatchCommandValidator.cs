using FluentValidation;
using Kogan.Mobile.Application.Batches.Commands.LoadBatch;
using Kogan.Mobile.Domain.Mobile.Enums;

namespace Application.Batches.Commands.LoadBatch
{
    public sealed class LoadBatchCommandValidator : AbstractValidator<LoadBatchFromFileCommand>
    {
        public LoadBatchCommandValidator()
        {
            this.RuleFor(r => r.Country)
                .NotEqual(VoucherCountryEnum.None)
                .WithMessage("Invalid batch country, it must either be from NZ or AU.");

            this.RuleFor(r => r.SimType)
                .NotEqual(MobileVoucherSimTypeEnum.NoSim)
                .WithMessage("Please provide a sim type for this batch of vouchers.");

            this.RuleFor(r => r.PlanSize)
                .NotEqual(MobileVoucherPlanSizeEnum.None)
                .WithMessage("Please provide a plan size (S, M, XL, [D]ata) for this batch of vouchers.");

            this.RuleFor(r => r.SimDistribution)
                .NotNull()
                .NotEmpty();

            this.RuleFor(r => r.PlanDurationDays)
                .GreaterThan(0);

            this.RuleFor(r => r.SimDistribution)
                .Must(simDistribution =>
                {
                    // One distribution per sim type
                    var gSimDistributionBySimType = simDistribution
                        .GroupBy(d => d.SimType);

                    var gDuplicateSimType = gSimDistributionBySimType
                        .FirstOrDefault(g => g.Skip(1).Any());

                    return gDuplicateSimType is null;
                })
                .WithMessage($"Invalid sim distribution, one sim type was set multiple time.");

            this.RuleFor(r => r.SimDistribution)
                .Must(simDistribution =>
                {
                    // Each sim distribution must have a unique WebSKU
                    var distinctWebSkus = simDistribution
                        .Select(sD => sD.WebSku)
                        .Distinct(StringComparer.OrdinalIgnoreCase);

                    return distinctWebSkus.Count() == simDistribution.Count();
                })
                .WithMessage($"Each sim distribution must have its own unique WebSKU.");
        }
    }

    public sealed class SimDistributionValidator : AbstractValidator<LoadBatchFromFileCommand.SimDistributionDefinition>
    {
        public SimDistributionValidator()
        {
            this.RuleFor(s => s.SimType)
                .NotEqual(MobileVoucherSimTypeEnum.None);

            this.RuleFor(s => s.TotalQuantity)
                .GreaterThan(0);

            this.RuleFor(s => s.WebSku)
                .NotNull()
                .NotEmpty();
        }
    }
}
