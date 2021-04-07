using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Infrastructure.WalletService.Openway.Models.Application;
using WalletConnector.Infrastructure.WalletService.Openway.Models.Information;

namespace WalletConnector.Infrastructure.WalletService.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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
