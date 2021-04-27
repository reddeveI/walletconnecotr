using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletConnector.Domain.Transactrions
{
    public class WithdrawalTransaction
    {
        public string Phone { get; set; }
        public string TransactionId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public string Currency { get; set; }
        public string MessageCode { get; set; }
        public string TransactionType { get; set; }
    }
}
