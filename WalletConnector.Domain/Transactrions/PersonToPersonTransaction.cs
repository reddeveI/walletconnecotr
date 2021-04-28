namespace WalletConnector.Domain.Transactrions
{
    public class PersonToPersonTransaction : CommonTransaction
    {
        public string From { get; set; } 
        public string To { get; set; }
        public string TransactionType { get; set; }
        public string TransactionId { get; set; }
    }
}
