using System.Xml.Linq;
using WalletConnectior.Xml.Models;

namespace WalletConnectior.Xml.Mapping
{
    public sealed class ApplicationMap
    {
        public ApplicationMap(XName elementName) => ElementName = elementName;
        
        public XName ElementName { get; }
        
        public XName Scheme { get; } = XNamespace.None.GetName("scheme");
        public XName MsgType { get; } = XNamespace.None.GetName("msg_type");
        public XName Direction { get; } = XNamespace.None.GetName("direction");
        public XName Version { get; } = XNamespace.None.GetName("version");

        public XName MsgId { get; } = nameof(MsgId);
        public XName Source { get; } = nameof(Source);
        
        public XName RegNumber { get; } = nameof(RegNumber);
        public XName Institution { get; } = nameof(Institution);
        public XName InstitutionIDType { get; } = nameof(InstitutionIDType);
        public XName OrderDprt { get; } = nameof(OrderDprt);
        public XName ObjectType { get; } = nameof(ObjectType);
        public XName ActionType { get; } = nameof(ActionType);
        public XName ProductCategory { get; } = nameof(ProductCategory);

        public XName MsgData { get; } = nameof(MsgData);
        
        public ObjectForMap ObjectForMap { get; } = new ObjectForMap(XNamespace.None.GetName("ObjectFor"));
        public ResultDtlsMap ResultDtlsMap { get; } = new ResultDtlsMap(XNamespace.None.GetName("ResultDtls"));
        
        public Application FromXml(XElement e)
        {
            var appEl = e.Element(MsgData)?.Element(ElementName);

            if (appEl == null)
                return null;
            
            return new Application
            {
                Scheme = e.Attribute(Scheme).AsString(),
                Direction = e.Attribute(Direction).AsString(),
                MsgType = e.Attribute(MsgType).AsString(),
                Version = e.Attribute(Version).AsString(),
                MsgId = e.Element(MsgId).AsGuid(),
                Source = e.Element(Source)?.Attribute("app").AsString(),
                RegNumber = appEl.Element(RegNumber).AsGuid(),
                Institution = appEl.Element(Institution).AsLong(),
                InstitutionIDType = appEl.Element(InstitutionIDType).AsString(),
                OrderDprt = appEl.Element(OrderDprt).AsLong(),
                ObjectType = appEl.Element(ObjectType).AsString(),
                ActionType = appEl.Element(ActionType).AsString(),
                ProductCategory = appEl.Element(ProductCategory).AsString(),
                ResultDtl = ResultDtlsMap.FromXml(appEl.Element(ResultDtlsMap.ElementName)),
                ObjectFor = ObjectForMap.FromXml(appEl.Element(ObjectForMap.ElementName))
            };
        }
    }
}