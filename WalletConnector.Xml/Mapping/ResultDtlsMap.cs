using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Mapping
{
    public class ResultDtlsMap
    {
        public ResultDtlsMap(XName elementName) => ElementName = elementName;
        
        public XName ElementName { get; }

        public ParmMap ParmMap { get; } = new ParmMap(XNamespace.None.GetName("Parm"));
        
        public List<Parm> FromXml(XElement e) =>
            e.Elements(ParmMap.ElementName).Select(parm => ParmMap.FromXml(parm)).ToList();
    }
}