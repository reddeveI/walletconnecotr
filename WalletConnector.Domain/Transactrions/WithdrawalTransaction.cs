namespace WalletConnector.Domain.Transactrions
{
    public class WithdrawalTransaction : CommonTransaction
    {
        public string Phone { get; set; }
        public string TransactionId { get; set; }
        public string Description { get; set; }
        public decimal Commission { get; set; }
        public string TransactionType { get; set; }
    }
}
