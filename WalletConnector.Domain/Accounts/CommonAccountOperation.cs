namespace WalletConnector.Domain.Accounts
{
    public abstract class CommonAccountOperation
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string MessageCode { get; set; }
        public string Phone { get; set; }
    }
}
