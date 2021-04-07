using System.Threading.Tasks;

using WalletConnectior.Xml.Serialization;

using Xunit;

namespace WalletConnector.Xml.Tests
{
    public class XmlTests: BaseTests
    {
        [Fact]
        public async Task Application_Deserialization_Test()
        {
            var xml = await GetXmlAsync("sample_application.xml");
            var app = ApplicationDeserializer.Deserialize(xml);
            
            Assert.NotNull(app);
            Assert.NotEmpty(app.ResultDtl);
            Assert.NotNull(app.ObjectFor);;
        }
    }
}