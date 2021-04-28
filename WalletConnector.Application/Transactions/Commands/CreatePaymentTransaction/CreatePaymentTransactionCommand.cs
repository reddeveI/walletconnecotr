using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Queries.CheckForPayment;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Domain.Transactrions;

namespace WalletConnector.Application.Transactions.Commands.CreatePaymentTransaction
{
    public record CreatePaymentTransactionCommand : IRequest<PaymentTransactionCreatedVm>
    {
        public UserTransfer UserTransfer { get; set; }

        public string MessageCode { get; private set; } = "account_transfer";

        public string TransactionType { get; private set; } = "W2M";
    }

    public class CreatePaymentTransactionCommandHandler : IRequestHandler<CreatePaymentTransactionCommand, PaymentTransactionCreatedVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CreatePaymentTransactionCommandHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<PaymentTransactionCreatedVm> Handle(CreatePaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            var paymentTransactionRequest = _mapper.Map<PaymentTransaction>(request);

            var walletResponse = await _walletService.CreatePaymentTransaction(paymentTransactionRequest, cancellationToken);

            if (walletResponse.Status != 0)
            {
                throw new BadRequestException();
            }

            var transactionCreatedResult = _mapper.Map<PaymentTransactionCreatedVm>(walletResponse);

            return transactionCreatedResult;
        }
    }
}
