using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Application.Accounts.Commands.CreateAccount
{
    public class AccountCreatedVm
    {
        public string Phone { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public Wallet Wallet { get; set; }
    }

    public class Wallet
    {
        public string Pan { get; set; }
    }
}
