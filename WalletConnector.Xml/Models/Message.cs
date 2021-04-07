namespace WalletConnectior.Xml.Models
{
    public class Message
    {
        public string Scheme { get; set; }
        
        public virtual string MsgType { get; set; }
        
        public string Direction { get; set; }
        
        public string Version { get; set; }
    }
}