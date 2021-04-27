using FluentValidation;

namespace WalletConnector.Application.Transactions.Commands.CreateExternalTransaction
{
    public class CreateExternalTransactionCommandValidator : AbstractValidator<CreateExternalTransactionCommand>
    {
        public CreateExternalTransactionCommandValidator()
        {
            RuleFor(v => v.User)
                .NotEmpty().WithMessage("Phone is required.")
                .MinimumLength(9).WithMessage("Phone must be more than 9 characters.")
                .MaximumLength(11).WithMessage("Phone must not exceed 11 characters.");

            RuleFor(v => v.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be 3 characters.");

            RuleFor(v => v.Amount)
               .NotEmpty()
               .GreaterThan(0);

            RuleFor(v => v.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required.")
                .MinimumLength(3).WithMessage("TransactionId must be more than 3 characters.")
                .MaximumLength(32).WithMessage("TransactionId must not exceed 32 characters.");
        }
    }
}
