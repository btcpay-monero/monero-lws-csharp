using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsGetRandomOutsResponse</c> models the random outputs to use
/// in a ring signature of a new transaction.
/// </summary>
public class MoneroLwsGetRandomOutsResponse
{
    /// <summary>
    ///  Dummy outputs for each amount(s). If there are not enough outputs to mix for a specific amount,
    /// the server shall omit the outputs field in amount_outs.
    /// </summary>
    [JsonPropertyName("amount_outs")]
    public List<MoneroLwsRandomOutput> AmountOuts { get; set; } = [];
}