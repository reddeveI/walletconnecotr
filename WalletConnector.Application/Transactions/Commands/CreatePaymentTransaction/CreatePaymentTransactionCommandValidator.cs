using FluentValidation;

namespace WalletConnector.Application.Transactions.Commands.CreatePaymentTransaction
{
    public class CreatePaymentTransactionCommandValidator : AbstractValidator<CreatePaymentTransactionCommand>
    {
        public CreatePaymentTransactionCommandValidator()
        {
            RuleFor(v => v.UserTransfer.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MinimumLength(9).WithMessage("UserId must be more than 9 characters.")
                .MaximumLength(11).WithMessage("UserId must not exceed 11 characters.");
        }
    }
}
