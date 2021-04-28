using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Application.Transactions.Commands.CreateTransaction
{
    public class TransactionCreatedVm
    {
        public TransferDetail Transfer { get; set; }

        public int Status { get; set; }
    }

    public class TransferDetail
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Currency { get; set; }

        public string Amount { get; set; }
    }
}
