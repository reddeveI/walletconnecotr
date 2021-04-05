using System;
using System.Collections.Generic;
using System.Text;

namespace WalletConnector.Application.Infrastructure.Services.WalletService
{
    public class AccountInfoResponseDto
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
