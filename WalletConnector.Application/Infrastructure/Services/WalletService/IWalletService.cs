using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;
using WalletConnector.Application.Transactions.Commands.CreateTransaction;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public interface IWalletService
    {
        Task<AccountInfoResponseDto> GetAccountInfo(string phone);

        Task<AccountCreatedVm> CreateAccount(string phone, string description, CancellationToken cancellationToken);

        Task<TransactionCreatedVm> CreateTransaction(string From, string To, string Amount, string Currency, CancellationToken cancellationToken);
    }
}
