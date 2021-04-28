using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Commands.UnholdAccount
{
    public record UnholdAccountCommand(long TransactionId, string Phone, decimal Amount) : IRequest<AccountUnholdedVm>;

    public class UnholdAccountCommandHandler : IRequestHandler<UnholdAccountCommand, AccountUnholdedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public UnholdAccountCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<AccountUnholdedVm> Handle(UnholdAccountCommand request, CancellationToken cancellationToken)
        {
            var messageCode = "WLT_CLOSE_EVNT";

            var unholdAccountRequest = await _walletService.UnholdAccount(request.Phone, request.Amount, "KZT", messageCode, cancellationToken);

            return unholdAccountRequest;
        }
    }
}
