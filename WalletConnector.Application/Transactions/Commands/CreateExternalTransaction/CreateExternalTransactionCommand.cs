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
            TransactionCreatedVm createTransactionRequest;

            try
            {
                var user = await _walletService.GetAccountInfo(request.User);
            }
            catch(NotFoundException) {
                await _walletService.CreateAccount(request.User, "Created via system", cancellationToken);
            }
            finally
            {
                var masterWallet = _keys.Value.Keys[request.Token];

                createTransactionRequest = await _walletService.CreateTransaction(masterWallet, request.User, request.Amount,
                    request.Currency, request.MessageCode, request.TransactionType, cancellationToken, request.TransactionId);

                if (createTransactionRequest.Status != 0)
                {
                    throw new BadRequestException();
                }
            }

            return createTransactionRequest;
        }
    }
}
