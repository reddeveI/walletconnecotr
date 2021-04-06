using System.Xml.Linq;

using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Mapping
{
    public class ObjectForMap
    {
        public ObjectForMap(XName elementName) => ElementName = elementName;
        public XName ElementName { get; }

        private const string ClientIDT = nameof(ClientIDT);
        
        public ClientInfoMap ClientInfoMap { get; } = new ClientInfoMap(XNamespace.None.GetName("ClientInfo"));

        public ClientInfo FromXml(XElement e)
        {
            return ClientInfoMap.FromXml(
                e.Element(ClientIDT)?
                    .Element(ClientInfoMap.ElementName)
                );
        }
    }
}