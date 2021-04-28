using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.DependencyInjection;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Domain.Transactrions;

namespace WalletConnector.Application.Transactions.Commands.CreateExternalTransaction
{
    public record CreateExternalTransactionCommand(
        string User, 
        string Currency, 
        decimal Amount, 
        string TransactionDt, 
        string TransactionId, 
        string Description,
        string Token,
        string MessageCode = "account_transfer",
        string TransactionType = "O2W"
        ) : IRequest<TransactionCreatedVm>;

    public class CreateExternalTransactionCommandHandler : IRequestHandler<CreateExternalTransactionCommand, TransactionCreatedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;
        private readonly IOptions<ClientKeys> _keys;

        public CreateExternalTransactionCommandHandler(IWalletService walletService, IMapper mapper, IOptions<ClientKeys> keys)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
            _keys = keys;
        }

        public async Task<TransactionCreatedVm> Handle(CreateExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            
            TransactionCreatedVm transactionCreatedResult;

            try
            {
                var user = await _walletService.GetAccountInfo(request.User);
            }
            catch(NotFoundException) {
                await _walletService.CreateAccount(request.User, "Created via system", cancellationToken);
            }
            finally
            {
                request = request with { Token = _keys.Value.Keys[request.Token] };
                var transactionRequest = _mapper.Map<PersonToPersonTransaction>(request);

                var walletResponse = await _walletService.CreateTransaction(transactionRequest, cancellationToken);

                if (walletResponse.Status != 0)
                {
                    throw new BadRequestException();
                }

                transactionCreatedResult = _mapper.Map<TransactionCreatedVm>(walletResponse);
            }

            return transactionCreatedResult;
        }
    }
}
