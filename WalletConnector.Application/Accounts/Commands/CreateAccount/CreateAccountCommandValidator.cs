using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WalletConnector.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MinimumLength(9).WithMessage("Phone must be more than 9 characters.")
                .MaximumLength(11).WithMessage("Phone must not exceed 11 characters.");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(3).WithMessage("Description must be more than 3 characters.")
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}
