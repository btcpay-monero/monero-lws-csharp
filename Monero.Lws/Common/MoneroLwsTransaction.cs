using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Class <c>MoneroLwsTransaction</c> models a monero wallet transaction.
/// </summary>
public class MoneroLwsTransaction
{
    /// <summary>
    /// Index of tx in blockchain. Is determined by the monero daemon.
    /// It is the offset that a transaction appears in the blockchain from the genesis block.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; } = 0;

    /// <summary>
    /// Bytes of tx hash, determined by how the monero core computes the hash.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = "";

    /// <summary>
    /// Timestamp of block. Null when <c>Mempool</c> is true.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public string? Timestamp { get; set; } = null;

    /// <summary>
    /// Total XMR received.
    /// </summary>
    [JsonPropertyName("total_received")]
    public string TotalReceived { get; set; } = "";

    /// <summary>
    /// XMR possibly being spent.
    /// </summary>
    [JsonPropertyName("total_sent")]
    public string TotalSent { get; set; } = "";

    /// <summary>
    /// Tx unlock time field.
    /// </summary>
    [JsonPropertyName("unlock_time")]
    public long UnlockTime { get; set; } = 0;

    /// <summary>
    /// Block height. Null when <c>Mempool</c> is true.
    /// </summary>
    [JsonPropertyName("height")]
    public long? Height { get; set; } = null;

    /// <summary>
    /// List of possible spends in this transaction only.
    /// </summary>
    [JsonPropertyName("spent_outputs")]
    public List<MoneroLwsSpend> SpentOutputs { get; set; } = [];

    /// <summary>
    /// Bytes of tx payment id. Is omitted if the transaction had none.
    /// It is decrypted when the encrypted form is used.
    /// The decryption may be incorrect - if the transaction was TO another address, then this will be random bytes.
    /// This happens frequently with outgoing payment ids.
    /// The received XMR in the transaction is change and the payment id is for the real recipient.
    /// </summary>
    [JsonPropertyName("payment_id")]
    public string? PaymentId { get; set; } = null;

    /// <summary>
    /// True if tx is coinbase.
    /// </summary>
    [JsonPropertyName("coinbase")]
    public bool Coinbase { get; set; } = false;

    /// <summary>
    /// True if tx is in mempool.
    /// </summary>
    [JsonPropertyName("mempool")]
    public bool Mempool { get; set; } = false;

    /// <summary>
    /// Mixin of the receiver. Does not include the real spend - this is the number of dummy inputs.
    /// </summary>
    [JsonPropertyName("mixin")]
    public int Mixin { get; set; } = 0;
}