using System.Xml.Linq;
using WalletConnectior.Xml.Mapping;
using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Serialization
{
    public static class ApplicationDeserializer
    {
        private const string UFXMsg = nameof(UFXMsg);
        
        public static Application Deserialize(string xml)
        {
            var e = XElement.Parse(xml);
            var appMap = new ApplicationMap(nameof(Application));
            return appMap.FromXml(e);
        }
    }
}