using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Domain.Transactrions;

namespace WalletConnector.Application.Transactions.Commands.CreateTransaction
{
    public record CreateTransactionCommand(string From, string To, decimal Amount, string Currency, 
        string MessageCode = "account_transfer", 
        string TransactionType = "W2W") : IRequest<TransactionCreatedVm> { }
    

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
            var transactionRequest = _mapper.Map<PersonToPersonTransaction>(request);

            var walletResponse = await _walletService.CreateTransaction(transactionRequest, cancellationToken);

            if (walletResponse.Status != 0)
            {
                throw new BadRequestException();
            }

            var transactionCreatedResult = _mapper.Map<TransactionCreatedVm>(walletResponse);

            return transactionCreatedResult;
        }
    }

}
