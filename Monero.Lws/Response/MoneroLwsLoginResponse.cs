using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsLoginResponse</c> models a response for account check or creation.
/// </summary>
public class MoneroLwsLoginResponse
{
    /// <summary>
    /// Whether account was just created.
    /// </summary>
    [JsonPropertyName("new_address")]
    public bool NewAddress { get; set; } = false;

    /// <summary>
    /// Flag from initial account creation.
    /// </summary>
    [JsonPropertyName("generated_locally")]
    public bool? GeneratedLocally { get; set; } = null;

    /// <summary>
    /// Account scanning start block.
    /// </summary>
    [JsonPropertyName("start_height")]
    public long? StartHeight { get; set; } = null;

    /// <summary>
    /// Account lookahead.
    /// </summary>
    [JsonPropertyName("lookahead")]
    public MoneroLwsAccountLookahead Lookahead { get; set; } = new();
}