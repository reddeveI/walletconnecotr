using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Commands.CreateAccount
{
    public record CreateAccountCommand(string Phone, string Description) : IRequest<AccountCreatedVm>;

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountCreatedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<AccountCreatedVm> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var walletResponse = await _walletService.CreateAccount(request.Phone, request.Description, cancellationToken);

            var createAccountResult = _mapper.Map<AccountCreatedVm>(walletResponse);

            return createAccountResult;
        }
    }
}
