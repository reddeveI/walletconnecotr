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
    public record CreateAccountCommand(string Phone, string Description) : IRequest<int>;

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var createAccountRequest = _walletService.CreateAccount(request.Phone, request.Description, cancellationToken);

            return createAccountRequest;
        }
    }
}
