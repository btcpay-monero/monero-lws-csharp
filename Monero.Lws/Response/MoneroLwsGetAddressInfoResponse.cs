using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsGetAddressInfoResponse</c> models the minimal set of information needed to calculate
/// a wallet balance. The server cannot calculate when a spend occurs without the spend key,
/// so a list of candidate spends is returned.
/// </summary>
public class MoneroLwsGetAddressInfoResponse
{
    /// <summary>
    /// Sum of unspendable XMR.
    /// </summary>
    [JsonPropertyName("locked_funds")]
    public string LockedFunds { get; set; } = "";

    /// <summary>
    /// Sum of received XMR.
    /// </summary>
    [JsonPropertyName("total_received")]
    public string TotalReceived { get; set; } = "";

    /// <summary>
    /// Sum of possibly spent XMR.
    /// </summary>
    [JsonPropertyName("total_sent")]
    public string TotalSent { get; set; } = "";

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
    /// Total txes sent in Monero.
    /// </summary>
    [JsonPropertyName("transaction_height")]
    public long TransactionHeight { get; set; } = 0;

    /// <summary>
    /// Current blockchain height.
    /// </summary>
    [JsonPropertyName("blockchain_height")]
    public long BlockchainHeight { get; set; } = 0;

    /// <summary>
    /// Possible spend info.
    /// </summary>
    [JsonPropertyName("spent_outputs")]
    public List<MoneroLwsSpend> SpentOutputs { get; set; } = [];

    /// <summary>
    /// Current exchange rates. Null if unavailable.
    /// </summary>
    [JsonPropertyName("rates")]
    public MoneroLwsRates? Rates { get; set; } = null;
}