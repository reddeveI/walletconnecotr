using FluentValidation;

namespace WalletConnector.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(v => v.From)
                .NotEmpty().WithMessage("PhoneFrom is required.")
                .MinimumLength(9).WithMessage("PhoneFrom must be more than 9 characters.")
                .MaximumLength(11).WithMessage("PhoneFrom must not exceed 11 characters.");

            RuleFor(v => v.To)
                .NotEmpty().WithMessage("PhoneTo is required.")
                .MinimumLength(9).WithMessage("PhoneTo must be more than 9 characters.")
                .MaximumLength(11).WithMessage("PhoneTo must not exceed 11 characters.");

            RuleFor(v => v.Amount)
               .NotEmpty()
               .GreaterThan(0);

            RuleFor(v => v.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be 3 characters.");
        }
    }
}
