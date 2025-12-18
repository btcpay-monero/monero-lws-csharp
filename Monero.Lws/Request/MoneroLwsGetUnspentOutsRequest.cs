using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

/// <summary>
/// Class <c>MoneroLwsGetUnspentOutsRequest</c> models a request for received outputs.
/// </summary>
public class MoneroLwsGetUnspentOutsRequest() : MoneroLwsWalletRequest()
{
    /// <summary>
    /// XMR send amount.
    /// </summary>
    [JsonPropertyName("amount")] public string Amount { get; set; } = "";
    /// <summary>
    /// Minimum mixin for source output.
    /// </summary>
    [JsonPropertyName("mixin")] public int Mixin { get; set; } = 0;
    /// <summary>
    /// Return all available outputs.
    /// </summary>
    [JsonPropertyName("use_dust")] public bool UseDust { get; set; } = true;
    /// <summary>
    /// Ignore outputs below this amount.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("dust_threshold")] public string? DustThreshold { get; set; } = null;
}
