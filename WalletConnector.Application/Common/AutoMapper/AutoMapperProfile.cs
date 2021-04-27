using AutoMapper;
using System.Linq;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Queries.GetAccountInfo;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Serializer.Models.Application;
using WalletConnector.Serializer.Models.Document;
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
                                Wallet = new()
                                {
                                    Balance = src.Wallet.Balance,
                                    Pan = src.Wallet.Balance,
                                    CardId  = src.Wallet.CardId,
                                    IssContractId = src.Wallet.IssContractId,
                                    UserData = new() 
                                    { 
                                        Iin = src.User.Iin, 
                                        FirstName = src.User.FirstName,
                                        LastName = src.User.LastName,
                                        MiddleName = src.User.MiddleName,
                                        Gender = src.User.Gender,
                                        BirthDate = src.User.BirthDate,
                                        Address = src.User.Address,
                                        City = src.User.City,
                                        DocumentNumber = src.User.DocumentNumber,
                                        DocumentType = src.User.DocumentType,
                                        IssuedBy = src.User.IssuedBy,
                                        IssueDate = src.User.IssueDate,
                                        ExpiryDate = src.User.ExpiryDate
                                    }
                                }
                            }
                        }));

            CreateMap<ApplicationRequest, AccountCreatedVm>()
                .ForPath(dest =>
                    dest.Phone,
                    opt => opt.MapFrom(src => src.MsgData.Application.SubApplList.SubApplication.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.CbsNumber))
                .ForPath(dest =>
                    dest.Currency,
                    opt => opt.MapFrom(src => src.MsgData.Application.SubApplList.SubApplication.DataRs.ContractRs.FirstOrDefault().RsContract.Currency));

            CreateMap<DocumentRequest, TransactionCreatedVm>()
                .ForPath(dest =>
                    dest.Status,
                    opt => opt.MapFrom(src => src.MsgData.Information.Status.RespCode));

            CreateMap<InformationRequest, AccountInfoResponseDto>()
                 .ForPath(dest =>
                    dest.Status,
                    opt => opt.MapFrom(src => int.Parse(src.MsgData.Information.Status.RespCode)))
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
                    dest.User.MiddleName,
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
                    dest.User.IssuedBy,
                    opt => opt.MapFrom(src => GetIssueData(src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.RegNumberDetails, 0)))
                .ForPath(dest =>
                    dest.User.IssueDate,
                    opt => opt.MapFrom(src => GetIssueData(src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.RegNumberDetails, 1)))
                .ForPath(dest =>                                                      
                    dest.User.ExpiryDate,                                             
                    opt => opt.MapFrom(src => GetIssueData(src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.Client.ClientInfo.RegNumberDetails, 2)))
                .ForPath(dest =>
                    dest.Wallet.Balance,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsInfo.Balances.Balance.Find(f => f.Type == "AVAILABLE").Amount.Replace(".", "")))
                .ForPath(dest =>
                    dest.Wallet.Pan,
                    opt => opt.MapFrom(src => src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.ContractIdt.ContractNumber))
                .ForPath(dest =>
                    dest.Wallet.CardId,
                    opt => opt.MapFrom(src => FormatResponseToCardId(src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.AddContractInfo.AddInfo02, "CardId=")))
                .ForPath(dest =>
                    dest.Wallet.IssContractId,
                    opt => opt.MapFrom(src => FormatResponseToCardId(src.MsgData.Information.DataRs.ContractRs.FirstOrDefault().RsContract.AddContractInfo.AddInfo02, "IssConractId=")));
        }

        private string FormatResponseToCardId(string text, string keyword)
        {
            if (text == null) return null;
            var result = text.Substring(text.IndexOf(keyword) + keyword.Length, 9);
            return result;
        }

        private string GetIssueData(string text, int index)
        {
            if (text == null)
            {
                return null;
            }
            return text.Split(";")[index];
        }
    }
}
