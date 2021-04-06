using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace WalletConnector.Xml.Tests
{
    public class BaseTests
    {
        protected async Task<string> GetXmlAsync(string fileName)
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));
            Assert.NotNull(resource);

            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            Assert.NotNull(stream);
            
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}