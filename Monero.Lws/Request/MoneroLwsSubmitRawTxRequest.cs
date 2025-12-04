using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

/// <summary>
/// Class <c>MoneroLwsSubmitRawTxRequest</c> models a request for submit a raw transaction to be relayed to monero
/// network.
/// </summary>
public class MoneroLwsSubmitRawTxRequest() : MoneroLwsRequest()
{
    /// <summary>
    /// Raw transaction bytes, in format used by daemon p2p comms.
    /// This format is tricky unfortunately, it is custom to the monero daemon.
    /// The internal code of monerod must be read to determine this format currently.
    /// </summary>
    [JsonPropertyName("tx")] public string Tx { get; set; } = "";
}
