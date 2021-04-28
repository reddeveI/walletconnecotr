using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using domain = WalletConnector.Domain.Accounts;

namespace WalletConnector.Application.Accounts.Commands.HoldAccount
{
    public record HoldAccountCommand(
        long TransactionId, 
        string Phone, 
        decimal Amount, 
        decimal Commission,
        string MessageCode = "WLT_CHECK_AV",
        string Srn = "ADONS0000001",
        string Currency = "KZT") : IRequest<AccountHoldedVm>;

    public class HoldAccountCommandHandler : IRequestHandler<HoldAccountCommand, AccountHoldedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public HoldAccountCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<AccountHoldedVm> Handle(HoldAccountCommand request, CancellationToken cancellationToken)
        {
            var holdAcountRequest = _mapper.Map<domain.HoldAccount>(request);

            var walletResponse = await _walletService.HoldAccount(holdAcountRequest, cancellationToken);

            var holdAccountResult = _mapper.Map<AccountHoldedVm>(walletResponse);

            return holdAccountResult;
        }
    }
}
