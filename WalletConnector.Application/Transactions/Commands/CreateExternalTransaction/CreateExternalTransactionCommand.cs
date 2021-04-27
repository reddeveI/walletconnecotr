using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;

namespace WalletConnector.Application.Transactions.Commands.CreateExternalTransaction
{
    public record CreateExternalTransactionCommand(
        string User, 
        string Currency, 
        decimal Amount, 
        string TransactionDt, 
        string TransactionId, 
        string Description
        ) : IRequest<TransactionCreatedVm>;

    public class CreateExternalTransactionCommandHandler : IRequestHandler<CreateExternalTransactionCommand, TransactionCreatedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CreateExternalTransactionCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<TransactionCreatedVm> Handle(CreateExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            //RGC060440002605
            //RGB060440002605
            //RPS060840003201
            var masterWallet = "RGC060440002605";
            var messageCode = "account_transfer";
            var transactionType = "O2W";

            var createTransactionRequest = await _walletService.CreateTransaction(masterWallet, request.User, request.Amount, request.Currency, messageCode, transactionType, cancellationToken, request.TransactionId);

            if (createTransactionRequest.Status != 0)
            {
                throw new BadRequestException();
            }

            return createTransactionRequest;
        }
    }

}
