using System.Xml.Linq;
using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Mapping
{
    public class ParmMap
    {
        public ParmMap(XName elementName) => ElementName = elementName;
        public XName ElementName { get; }
        public XName ParmCode { get; } = nameof(ParmCode);
        public XName Value { get; } = nameof(Value);

        public Parm FromXml(XElement e) => 
            new Parm
            {
                ParmCode = e.Element(ParmCode).AsString(),
                Value = e.Element(Value).AsString()
           };
    }
}