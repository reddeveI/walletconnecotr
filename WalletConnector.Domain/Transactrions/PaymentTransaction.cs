using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Domain.Transactrions
{
    public class PaymentTransaction : CommonTransaction
    {
        public string From { get; set; }
        public string To { get; set; }

        public string TransactionId { get; set; }

        public decimal Commission { get; set; }

        public string ProviderName { get; set; }

        public string TransactionType { get; set; }
    }
}
