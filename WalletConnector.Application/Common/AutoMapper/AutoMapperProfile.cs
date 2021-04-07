using AutoMapper;
using System.Linq;
using WalletConnector.Application.Accounts.Queries.GetAccountInfo;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Serializer.Models.Information;
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


            CreateMap<InformationRequest, AccountInfoResponseDto>()
                .ForPath(dest =>
                    dest.User.Phone,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.ContractNumber))
                .ForPath(dest =>
                    dest.User.Iin,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.TaxpayerIdentifier))
                .ForPath(dest =>
                    dest.User.LastName,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.LastName))
                .ForPath(dest =>
                    dest.User.FirstName,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.FirstName))
                .ForPath(dest =>
                    dest.User.Phone,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.MiddleName))
                .ForPath(dest =>
                    dest.User.Gender,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.Gender))
                .ForPath(dest =>
                    dest.User.BirthDate,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.BirthDate))
                .ForPath(dest =>
                    dest.User.Address,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.BaseAddress.AddressLine1))
                .ForPath(dest =>
                    dest.User.City,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.BaseAddress.City))
                .ForPath(dest =>
                    dest.User.DocumentNumber,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.RegNumber))
                .ForPath(dest =>
                    dest.Wallet.Balance,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsInfo.Balances.Balance.Find(f => f.Type == "AVAILABLE").Amount.Replace(".", "")));
        }
    }
}
