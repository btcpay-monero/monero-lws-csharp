using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Class <c>MoneroLwsRandomOutputs</c> models a randomly selected outputs for use in a ring signature.
/// </summary>
public class MoneroLwsRandomOutputs
{
    /// <summary>
    /// XMR amount, 0 if ringct.
    /// </summary>
    [JsonPropertyName("amount")] public string Amount { get; set; } = "";
    /// <summary>
    /// Selected outputs. Is omitted by the server if the <c>Amount</c> does not have enough mixable outputs.
    /// </summary>
    [JsonPropertyName("outputs")] public List<MoneroLwsRandomOutput>? Outputs { get; set; } = null;
}
