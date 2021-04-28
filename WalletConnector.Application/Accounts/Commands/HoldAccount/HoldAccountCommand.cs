using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Commands.HoldAccount
{
    public record HoldAccountCommand(long TransactionId, string Phone, decimal Amount, decimal Commission) : IRequest<AccountHoldedVm>;

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
            var messageCode = "WLT_CHECK_AV";
            var transactionId = "ADONS0000001";

            var holdAccountRequest = await _walletService.HoldAccount(request.Phone, request.Amount + request.Commission, "KZT", messageCode, transactionId, cancellationToken);

            return holdAccountRequest;
        }
    }
}
