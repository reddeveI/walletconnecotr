using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WalletConnector.Application.Infrastructure.Services.WalletService;

namespace WalletConnector.Application.Accounts.Queries.GetAccountInfo
{
    public class AccountInfoVm
    {
        public List<ActualWallet> Actual { get; set; }

        public class ActualWallet
        {
            public UserWallet Wallet { get; set; }

        }

        public class UserWallet
        {
            public string Balance { get; set; }

            public UserData UserData { get; set; } 
        }

        public class UserData
        {
            public string Iin { get; set; }

            public string FirstName { get; set; }
        }
    }
}
