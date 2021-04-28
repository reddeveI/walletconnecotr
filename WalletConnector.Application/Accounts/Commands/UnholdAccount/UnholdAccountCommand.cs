using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using domain = WalletConnector.Domain.Accounts;

namespace WalletConnector.Application.Accounts.Commands.UnholdAccount
{
    public record UnholdAccountCommand(
        long TransactionId, 
        string Phone, 
        decimal Amount,
        string MessageCode = "WLT_CLOSE_EVNT",
        string Currency = "KZT") : IRequest<AccountUnholdedVm>;

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
            var unholdAccountRequest = _mapper.Map<domain.UnholdAccount>(request);

            var walletResponse = await _walletService.UnholdAccount(unholdAccountRequest, cancellationToken);

            var holdAccountResult = _mapper.Map<AccountUnholdedVm>(walletResponse);

            return holdAccountResult;
        }
    }
}
