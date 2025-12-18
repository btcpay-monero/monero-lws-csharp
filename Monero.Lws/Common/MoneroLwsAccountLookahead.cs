using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Models an account subaddress lookahead.
/// </summary>
public class MoneroLwsAccountLookahead
{
    /// <summary>
    /// Account index lookahead.
    /// </summary>
    [JsonPropertyName("maj_i")] public long MajorIndex { get; set; } = 0;
    /// <summary>
    /// Subaddress index lookahead.
    /// </summary>
    [JsonPropertyName("min_i")] public long MinorIndex { get; set; } = 0;
}