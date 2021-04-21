using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Queries.GetAccountInfo
{
    public record GetAccountInfoQuery(string Phone) : IRequest<AccountInfoVm>;

    public class GetAccountInfoQueryHandler : IRequestHandler<GetAccountInfoQuery, AccountInfoVm>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public GetAccountInfoQueryHandler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _mapper = mapper;
        }

        public async Task<AccountInfoVm> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var getInfoRequest = await _walletService.GetAccountInfo(request.Phone);

            return _mapper.Map<AccountInfoVm>(getInfoRequest);
        }
    }
}
