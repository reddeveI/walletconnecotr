using System.Xml.Linq;
using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Mapping
{
    public class ClientInfoMap
    {
        public ClientInfoMap(XName elementName) => ElementName = elementName;
        
        public XName ElementName { get; }
        
        public XName ClientNumber { get; } = nameof(ClientNumber);
        public XName ShortName { get; } = nameof(ShortName);

        public ClientInfo FromXml(XElement e)
        {
            return new ClientInfo
            {
                ClientNumber = e.Element(ClientNumber).AsLong(),
                ShortName = e.Element(ShortName).AsLong()
            };
        }
    }
}