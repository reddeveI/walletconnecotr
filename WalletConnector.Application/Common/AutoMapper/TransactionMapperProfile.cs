using AutoMapper;
using System.Linq;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Accounts.Commands.HoldAccount;
using WalletConnector.Application.Accounts.Commands.UnholdAccount;
using WalletConnector.Application.Accounts.Queries.CheckForPayment;
using WalletConnector.Application.Accounts.Queries.GetAccountInfo;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Application.Transactions.Commands.CreateExternalTransaction;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;
using WalletConnector.Application.Transactions.Commands.CreateWithdrawalTransaction;
using WalletConnector.Domain.Accounts;
using WalletConnector.Domain.Transactrions;
using WalletConnector.Serializer.Models.Application;
using WalletConnector.Serializer.Models.Document;
using WalletConnector.Serializer.Models.Information;
using static WalletConnector.Application.Accounts.Queries.GetAccountInfo.AccountInfoVm;

namespace WalletConnector.Application.Common.AutoMapper
{
    public class TransactionMapperProfile : Profile
    {
        public TransactionMapperProfile()
        {
            CreateMap<CreateTransactionCommand, PersonToPersonTransaction>();

            CreateMap<DocumentRequest, PersonToPersonTransactionCreated>()
                .ForPath(dest =>
                    dest.Status,
                    opt => opt.MapFrom(src => src.MsgData.Information.Status.RespCode));

            CreateMap<PersonToPersonTransactionCreated, TransactionCreatedVm>();


            CreateMap<CreateExternalTransactionCommand, PersonToPersonTransaction>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.User));


            CreateMap<CreateWithdrawalTransactionCommand, WithdrawalTransaction>();

            CreateMap<DocumentRequest, WithdrawalTransactionCreated>()
                .ForPath(dest =>
                    dest.Status,
                    opt => opt.MapFrom(src => src.MsgData.Information.Status.RespCode));
        }
    }
}
