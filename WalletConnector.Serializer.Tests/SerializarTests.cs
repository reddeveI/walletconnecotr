using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using WalletConnector.Serializer.Models.Application;
using WalletConnector.Serializer.Models.Information;
using Xunit;

namespace WalletConnector.Serializer.Tests
{
    public class SerializarTests
    {
        private async Task<string> GetXmlAsync(string fileName)
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));
            Assert.NotNull(resource);

            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            Assert.NotNull(stream);

            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }
        
        [Fact]
        public async Task DeserializeApplication()
        {
            var xml = await GetXmlAsync("Application_1.xml");
            Assert.NotNull(xml);
            Assert.NotEmpty(xml);
            
            var application = xml.FromXElement<ApplicationRequest>();
            Assert.NotNull(application);
            Assert.NotEmpty(application.Direction);
        }
        
        [Fact]
        public async Task DeserializeInformation()
        {
            var informationEntity = InformationBuilder.CreateDefaultInformation().AddPhoneNumber("77717545421")
                .AddResultDetails();

            var xml = await GetXmlAsync("Information_1.xml");
            Assert.NotNull(xml);
            Assert.NotEmpty(xml);
            
            var information = xml.FromXElement<InformationRequest>();

            informationEntity.MsgId = information.MsgId;
            informationEntity.MsgData.Information.RegNumber = information.MsgData.Information.RegNumber;
            
            information.Should().NotBeNull();
            information.Should().BeEquivalentTo(informationEntity);
        }
    }
}