using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Public monero address indices.
/// </summary>
public class MoneroLwsAddressMeta
{
    /// <summary>Subaddress major index</summary>
    [JsonPropertyName("maj_i")] public long MajIndex { get; set; } = 0;
    /// <summary>Subaddress minor index</summary>
    [JsonPropertyName("min_i")] public long MinIndex { get; set; } = 0;
}