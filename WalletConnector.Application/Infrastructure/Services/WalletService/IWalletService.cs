using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public interface IWalletService
    {
        Task<AccountInfoResponseDto> GetAccountInfo(string phone);

        Task<int> CreateAccount(string phone, string description);
    }
}
