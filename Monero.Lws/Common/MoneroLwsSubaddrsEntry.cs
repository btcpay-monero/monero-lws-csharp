using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Models a subaddrs object.
/// </summary>
public class MoneroLwsSubaddrsEntry
{
    /// <summary>
    /// Major index of Monero subaddresses.
    /// </summary>
    [JsonPropertyName("key")] public long AccountIndex { get; set; } = 0;
    /// <summary>
    /// Minor indexes of subaddresses within the major index.
    /// </summary>
    [JsonPropertyName("value")] public List<List<long>> Ranges { get; set; } = [];
}