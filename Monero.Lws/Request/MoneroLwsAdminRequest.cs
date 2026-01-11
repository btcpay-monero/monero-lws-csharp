using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

public class MoneroLwsAdminRequest : MoneroLwsRequest
{
    /// <summary>
    /// Lws admin authentication key.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("auth")]
    public string? Auth { get; set; } = null;

    /// <summary>
    /// Lws admin request parameters.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("params")]
    public MoneroLwsAdminParams? Params { get; set; } = null;
}