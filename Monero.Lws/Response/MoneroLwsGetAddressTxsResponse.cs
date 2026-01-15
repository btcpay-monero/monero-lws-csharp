using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsGetAddressTxsResponse</c> models the information needed to show transaction history.
/// The server cannot calculate when a spend occurs without the spend key, so a list of candidate spends is returned.
/// </summary>
public class MoneroLwsGetAddressTxsResponse
{
    /// <summary>
    /// Sum of received outputs.
    /// </summary>
    [JsonPropertyName("total_received")]
    public string TotalReceived { get; set; } = "";

    /// <summary>
    /// Current tx scan progress.
    /// </summary>
    [JsonPropertyName("scanned_height")]
    public long ScannedHeight { get; set; } = 0;

    /// <summary>
    /// Current scan progress.
    /// </summary>
    [JsonPropertyName("scanned_block_height")]
    public long ScannedBlockHeight { get; set; } = 0;

    /// <summary>
    /// Start height of response.
    /// </summary>
    [JsonPropertyName("start_height")]
    public long StartHeight { get; set; } = 0;

    /// <summary>
    /// Current blockchain height.
    /// </summary>
    [JsonPropertyName("blockchain_height")]
    public long BlockchainHeight { get; set; } = 0;

    /// <summary>
    /// Possible spend info.
    /// </summary>
    [JsonPropertyName("transactions")]
    public List<MoneroLwsTransaction> Transactions { get; set; } = [];
}