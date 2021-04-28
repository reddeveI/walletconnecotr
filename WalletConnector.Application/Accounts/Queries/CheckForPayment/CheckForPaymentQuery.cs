using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Common.Exceptions;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Queries.CheckForPayment
{
    public record CheckForPaymentQuery : IRequest<PaymentCheckVm>
    {
        public UserTransfer UserTransfer { get; set; }
    }

    public class UserTransfer
    {
        public string RequestId { get; set; }

        public string UserId { get; set; }

        public string UserData { get; set; }

        public decimal Amount { get; set; }

        public decimal ProvAmount { get; set; }

        public decimal Fee { get; set; }

        public string ProvTime { get; set; }

        public string Currency { get; set; }

        public Prov Prov { get; set; }
    }

    public class Prov
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Bin { get; set; }

        public string ProviderId { get; set; }

        public string Account { get; set; }

        public string Bik { get; set; }
    }

    public class CheckForPaymentQueryHandler : IRequestHandler<CheckForPaymentQuery, PaymentCheckVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public CheckForPaymentQueryHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<PaymentCheckVm> Handle(CheckForPaymentQuery request, CancellationToken cancellationToken)
        {
            var walletResponse = await _walletService.GetAccountInfo(request.UserTransfer.UserId);

            if (decimal.Parse(walletResponse.Wallet.Balance) <= request.UserTransfer.Amount)
            {
                throw new BadRequestException();
            }

            return _mapper.Map<PaymentCheckVm>(walletResponse);
        }
    }
}
