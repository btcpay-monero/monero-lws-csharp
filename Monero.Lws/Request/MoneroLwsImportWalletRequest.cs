using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Request;

/// <summary>
/// Class <c>MoneroLwsImportWalletRequest</c> models an account scan request from a specific block.
/// </summary>
public class MoneroLwsImportWalletRequest : MoneroLwsWalletRequest
{
    /// <summary>
    /// Restore wallet height.
    /// </summary>
    [JsonPropertyName("from_height")]
    public long FromHeight { get; set; } = 0;

    /// <summary>
    /// Desired lookahead for (re)scan.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("lookahead")]
    public MoneroLwsAccountLookahead? Lookahead { get; set; } = null;
}