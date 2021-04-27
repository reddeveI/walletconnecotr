using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction
{
    public record CreateWithdrawalTransactionCommand(string Phone, decimal Amount, decimal Commission, string Description, string CustomerReference) : IRequest<WithdrawalCreatedVm>;

    public class CreateWithdrawalTransactionCommandHandler : IRequestHandler<CreateWithdrawalTransactionCommand, WithdrawalCreatedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CreateWithdrawalTransactionCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<WithdrawalCreatedVm> Handle(CreateWithdrawalTransactionCommand request, CancellationToken cancellationToken)
        {
            var messageCode = "OrdReq";
            var transactionType = "CHTR";

            var createWithdrawalRequest = await _walletService.CreateWithdrawal(request.Phone, 
                request.CustomerReference, request.Description, request.Amount, request.Commission, 
                "KZT", messageCode, transactionType, cancellationToken);

            if (createWithdrawalRequest.Status != 0)
            {
                throw new BadRequestException();
            }

            return createWithdrawalRequest;
        }
    }
}
