using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Domain.Accounts
{
    public class AccountCreated
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
