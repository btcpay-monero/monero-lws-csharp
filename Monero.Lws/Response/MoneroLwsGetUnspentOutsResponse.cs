using System.Text.Json.Serialization;
using Monero.Lws.Common;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsGetUnspentOutsResponse</c> models a list of received outputs.
/// The client must determine when the output was actually spent.
/// </summary>
public class MoneroLwsGetUnspentOutsResponse
{
    /// <summary>
    /// Estimated network fee.
    /// </summary>
    [JsonPropertyName("per_byte_fee")] public string PerByteFee { get; set; } = "";
    /// <summary>
    /// Fee quantization mask.
    /// </summary>
    [JsonPropertyName("fee_mask")] public string FeeMask { get; set; } = "";
    /// <summary>
    /// The total value in outputs.
    /// </summary>
    [JsonPropertyName("amount")] public string Amount { get; set; } = "";
    /// <summary>
    /// Outputs possibly available for spending.
    /// </summary>
    [JsonPropertyName("outputs")] public List<MoneroLwsOutput> Outputs { get; set; } = [];
}
