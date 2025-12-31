using System.Text.Json.Serialization;

namespace Monero.Lws.Response;

public class MoneroLwsUpdateResponse
{
    /// <summary>
    /// Updated account addresses.
    /// </summary>
    [JsonPropertyName("updated")] public List<string> UpdatedAddresses { get; set; } = [];
}
