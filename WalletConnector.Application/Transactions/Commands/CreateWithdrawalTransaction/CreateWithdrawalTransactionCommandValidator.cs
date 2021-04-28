using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction
{
    public class CreateWithdrawalTransactionCommandValidator : AbstractValidator<CreateWithdrawalTransactionCommand>
    {
        public CreateWithdrawalTransactionCommandValidator()
        {
            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MinimumLength(9).WithMessage("Phone must be more than 9 characters.")
                .MaximumLength(11).WithMessage("Phone must not exceed 11 characters.");

            RuleFor(v => v.Amount)
               .NotEmpty()
               .GreaterThan(0);

            RuleFor(v => v.Commission)
               .NotEmpty();

            RuleFor(v => v.Description);

            RuleFor(v => v.CustomerReference)
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(v => v.Amount)
               .NotEmpty()
               .GreaterThan(0);
        }
    }
}
