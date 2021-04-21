using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletConnector.Application.Accounts.Commands.CreateAccount;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public interface IWalletService
    {
        Task<AccountInfoResponseDto> GetAccountInfo(string phone);

        Task<AccountCreatedVm> CreateAccount(string phone, string description, CancellationToken cancellationToken);
    }
}
