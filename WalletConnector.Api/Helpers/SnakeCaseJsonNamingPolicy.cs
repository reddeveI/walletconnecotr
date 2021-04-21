using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace WalletConnector.Api.Helpers
{
    public class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        private readonly SnakeCaseNamingStrategy _newtonsoftSnakeCaseNamingStrategy
            = new();

        public static SnakeCaseJsonNamingPolicy Instance { get; } = new();

        public override string ConvertName(string name) =>
            _newtonsoftSnakeCaseNamingStrategy.GetPropertyName(name, false);
    }
}
