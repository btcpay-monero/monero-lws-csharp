using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Class <c>MoneroLwsRandomOutput</c> models a random output to be used in a new transaction.
/// </summary>
public class MoneroLwsRandomOutput
{
    /// <summary>
    /// Index within amount. Is determined by the monero daemon.
    /// It is the offset from the first time the amount appeared in the blockchain.
    /// After ringct, this is the order of outputs as they appear in the blockchain.
    /// </summary>
    [JsonPropertyName("global_index")]
    public string GlobalIndex { get; set; } = "";

    /// <summary>
    /// Bytes of output public key.
    /// </summary>
    [JsonPropertyName("public_key")]
    public string PublicKey { get; set; } = "";

    /// <summary>
    /// Bytes containing ringct commitment.
    /// </summary>
    [JsonPropertyName("rct")]
    public string Rct { get; set; } = "";
}