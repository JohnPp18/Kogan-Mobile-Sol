using FluentValidation;

namespace Application.Commands.Batches.DeleteBatch
{
    public class DeleteBatchCommandValidator : AbstractValidator<DeleteBatchCommand>
    {
        public DeleteBatchCommandValidator()
        {
            this.RuleFor(b => b.Id)
                .GreaterThan(0);
        }
    }
}
