using System.Xml.Linq;
using WalletConnectior.Xml.Mapping;
using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Serialization
{
    public static class ApplicationDeserializer
    {        
        public static Application Deserialize(string xml) => 
            new ApplicationMap(nameof(Application)).FromXml(XElement.Parse(xml));
    }
}
