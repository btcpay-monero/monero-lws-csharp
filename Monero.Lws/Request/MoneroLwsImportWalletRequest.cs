using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

/// <summary>
/// Class <c>MoneroLwsImportWalletRequest</c> models an account scan request from a specific block.
/// </summary>
public class MoneroLwsImportWalletRequest() : MoneroLwsWalletRequest()
{
    [JsonPropertyName("from_height")] public long FromHeight { get; set; } = 0;
}