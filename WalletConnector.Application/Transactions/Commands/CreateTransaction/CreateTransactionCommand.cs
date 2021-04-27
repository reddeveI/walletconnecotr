using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Transactions.Commands.CreateTransaction
{
    public record CreateTransactionCommand(string From, string To, decimal Amount, string Currency) : IRequest<TransactionCreatedVm>;

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionCreatedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<TransactionCreatedVm> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var messageCode = "account_transfer";
            var transactionType = "W2W";

            var createTransactionRequest = await _walletService.CreateTransaction(request.From, request.To, request.Amount, request.Currency, messageCode, transactionType, cancellationToken, transactionId: null);

            if (createTransactionRequest.Status != 0)
            {
                throw new BadRequestException();
            }

            return createTransactionRequest;
        }
    }

}
