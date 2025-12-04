using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

/// <summary>
/// Selects random outputs to use in a ring signature of a new transaction.
/// If the amount is 0 then the monerod RPC get_output_distribution should be used to locally select outputs
/// using a gamma distribution as described in "An Empirical Analysis of Traceability in the Monero Blockchain".
/// If the amount is not 0, then the monerod RPC get_output_histogram should be used to locally select outputs using
/// a triangular distribution (uint64_t dummy_out = histogram.total * sqrt(float64(random_uint53) / float64(2^53))).
/// </summary>
public class MoneroLwsGetRandomOutsRequest(): MoneroLwsRequest()
{
    /// <summary>
    /// Mixin (name is historical). Clients must use amount 0 when computing a ringct output.
    /// </summary>
    [JsonPropertyName("count")] public long Count { get; set; } = 0;
    /// <summary>
    /// XMR amounts that need mixing. If clients are creating multiple rings with the same amount,
    /// they must set count to the mixin level and add the value to amounts multiple times.
    /// Server must respond to each value in amounts, even if the value appears multiple times.
    /// </summary>
    [JsonPropertyName("amounts")] public List<string> Amounts { get; set; } = [];
}
