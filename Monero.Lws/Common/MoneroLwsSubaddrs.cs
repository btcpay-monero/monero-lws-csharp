using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

public class MoneroLwsSubaddrs
{
    [JsonPropertyName("all_subaddrs")] public List<MoneroLwsSubaddrsEntry> AllSubaddrs { get; set; } = [];
    [JsonPropertyName("new_subaddrs")] public List<MoneroLwsSubaddrsEntry> NewSubaddrs { get; set; } = [];
}