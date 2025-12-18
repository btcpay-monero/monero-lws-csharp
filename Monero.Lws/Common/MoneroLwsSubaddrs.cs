using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

public class MoneroLwsSubaddrs
{
    /// <summary>
    /// All subaddresses provisioned for the wallet (including new).
    /// </summary>
    [JsonPropertyName("all_subaddrs")] public List<MoneroLwsSubaddrsEntry> AllSubaddrs { get; set; } = [];
    /// <summary>
    /// All new subaddresses provisioned in the request.
    /// </summary>
    [JsonPropertyName("new_subaddrs")] public List<MoneroLwsSubaddrsEntry> NewSubaddrs { get; set; } = [];
    /// <summary>
    /// Maximum number of subaddresses permitted by server.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("max_subaddresses")] public long? MaxSubaddresses { get; set; } = null;
}