using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Queries.GetAccountInfo
{
    public class AccountInfoVm
    {
        public ActualWallet Actual { get; set; }

        public class ActualWallet
        {
            public List<Wallet> Wallets { get; set; }

        }

        public class Wallet
        {
            public string Pan { get; set; }
        }
    }
}
