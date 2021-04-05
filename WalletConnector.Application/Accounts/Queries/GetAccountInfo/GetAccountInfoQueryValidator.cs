using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WalletConnector.Application.Accounts.Queries.GetAccountInfo
{
    public class GetAccountInfoQueryValidator : AbstractValidator<GetAccountInfoQuery>
    {

        public GetAccountInfoQueryValidator()
        {
            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MinimumLength(9).WithMessage("Phone must not exceed 9 characters.")
                .MaximumLength(11).WithMessage("Phone must not exceed 11 characters.");
                
        }
    }
}
