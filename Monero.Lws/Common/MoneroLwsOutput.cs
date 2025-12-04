using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Class <c>MoneroLwsOutput</c> models the information needed to spend an output.
/// </summary>
public class MoneroLwsOutput
{
    /// <summary>
    /// Index of tx in blockchain.
    /// </summary>
    /// <remarks>
    /// <c>TxId</c> is determined by the monero daemon. It is the offset that a transaction appears in the blockchain from the genesis block.
    /// </remarks>
    [JsonPropertyName("tx_id")] public long TxId { get; set; } = 0;
    /// <summary>XMR value of output.</summary>
    [JsonPropertyName("amount")] public string Amount { get; set; } = "";
    /// <summary>Index within vout vector.</summary>
    [JsonPropertyName("index")] public long Index { get; set; } = 0;
    /// <summary>Index within amount.</summary>
    /// <remarks>
    /// <c>GlobalIndex</c> is determined by the monero daemon.
    /// It is the offset from the first time the amount appeared in the blockchain.
    /// After ringct, this is the order of outputs as they appear in the blockchain.
    /// </remarks>
    [JsonPropertyName("global_index")] public long GlobalIndex { get; set; } = 0;
    /// <summary>Bytes of ringct data.</summary>
    /// <remarks>
    /// <c>Rct</c> is, for ringct outputs, a 96-byte blob containing the concatenation of the public commitment,
    /// then the ringct mask value, and finally the ringct amount value.
    /// For ringct coinbase outputs, the mask is always the identity mask and the amount is zero;
    /// for non-coinbase ringct outputs, the mask and amount are the respective raw encrypted values,
    /// which must be decrypted by the client using the view secret key. For non-ringct outputs, this field is nil.
    /// </remarks>
    [JsonPropertyName("rct")] public string Rct { get; set; } = "";
    /// <summary>Bytes of tx hash.</summary>
    /// <remarks><c>TxHash</c> is determined by how monerod computes the hash.</remarks>
    [JsonPropertyName("tx_hash")] public string TxHash { get; set; } = "";
    /// <summary>Bytes of tx prefix hash.</summary>
    /// <remarks><c>TxPrefixHash</c> is determined by how monerod computes the hash.</remarks>
    [JsonPropertyName("tx_prefix_hash")] public string TxPrefixHash { get; set; } = "";
    /// <summary>Bytes of output public key.</summary>
    [JsonPropertyName("public_key")] public string PublicKey { get; set; } = "";
    /// <summary>Bytes of the tx public key.</summary>
    [JsonPropertyName("tx_pub_key")] public string TxPubKey { get; set; } = "";
    /// <summary>Bytes of key images.</summary>
    [JsonPropertyName("spend_key_images")] public List<string> SpendKeyImages { get; set; } = [];
    /// <summary>Timestamp of containing block.</summary>
    [JsonPropertyName("timestamp")] public string Timestamp { get; set; } = "";
    /// <summary>Containing block height.</summary>
    [JsonPropertyName("height")] public long Height { get; set; } = 0;
    /// <summary>Address data of the recipient.</summary>
    [JsonPropertyName("recipient")] public MoneroLwsAddressMeta Recipient { get; set; } = new();
}
