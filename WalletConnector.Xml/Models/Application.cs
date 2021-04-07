using System;
using System.Collections.Generic;

namespace WalletConnectior.Xml.Models
{
    public class Application: Message
    {
        public override string MsgType => "Application";
        
        public Guid MsgId { get; set; }
        
        public string Source { get; set; }
        
        public Guid RegNumber { get; set; }
        
        public long Institution { get; set; }
        
        public string InstitutionIDType { get; set; }
        
        public long OrderDprt { get; set; }
        
        public string ObjectType { get; set; }
        
        public string ActionType { get; set; }
        
        public List<Parm> ResultDtl { get; set; }
        
        public string ProductCategory { get; set; }
        
        public ClientInfo ObjectFor { get; set; }
    }

    public class Parm
    {
        public string ParmCode { get; set; }
        
        public string Value { get; set; }
    }

    public class ClientInfo
    {
        public long ClientNumber { get; set; }
        
        public long ShortName { get; set; }
    }
}