using AutoMapper;
using System.Linq;
using WalletConnector.Application.Accounts.Queries.GetAccountInfo;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using static WalletConnector.Application.Accounts.Queries.GetAccountInfo.AccountInfoVm;

namespace WalletConnector.Application.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountInfoResponseDto, AccountInfoVm>()
                .ForPath(dest =>
                    dest.Actual,
                    opt => opt.MapFrom(src =>
                        new[]
                        {
                            new ActualWallet
                            {
                                Wallet = new UserWallet
                                {
                                    Balance = src.Wallet.Balance,
                                    UserData = new UserData { Iin = src.User.Iin, FirstName = src.User.FirstName }
                                }
                            }
                        }));
        }
    }
}
