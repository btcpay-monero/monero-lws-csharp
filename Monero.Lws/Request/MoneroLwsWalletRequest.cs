using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

/// <summary>
/// Class <c>MoneroLwsWalletRequest</c> models a wallet request to lws.
/// </summary>
public class MoneroLwsWalletRequest() : MoneroLwsRequest()
{
    /// <summary>
    /// Base58 address to retrieve. If is not authorized, the server return HTTP 403 "Forbidden" error.
    /// </summary>
    [JsonPropertyName("address")] public string Address { get; set; } = "";
    /// <summary>
    /// View key bytes for authorization.
    /// </summary>
    [JsonPropertyName("view_key")] public string ViewKey { get; set; } = "";
}
