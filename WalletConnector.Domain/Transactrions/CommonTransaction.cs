namespace WalletConnector.Domain.Transactrions
{
    public abstract class CommonTransaction
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string MessageCode { get; set; }
    }
}
