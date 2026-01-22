using System.Text.Json.Serialization;

namespace Monero.Lws.IntegrationTests.Utils;

internal class WalletInfo
{
    [JsonPropertyName("primaryAddress")] public string PrimaryAddress { get; set; } = "";
    [JsonPropertyName("publicViewKey")] public string PublicViewKey { get; set; } = "";
    [JsonPropertyName("publicSpendKey")] public string PublicSpendKey { get; set; } = "";
    [JsonPropertyName("privateViewKey")] public string PrivateViewKey { get; set; } = "";
}