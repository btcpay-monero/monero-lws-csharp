using System.Text.Json.Serialization;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsStatusResponse</c> models a generic status response from lws.
/// </summary>
public class MoneroLwsStatusResponse
{
    /// <summary>
    /// Custom message.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = "";
}