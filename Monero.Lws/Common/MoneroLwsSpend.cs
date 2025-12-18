using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Class <c>MoneroLwsSpend</c> model a possible spent output.
/// </summary>
public class MoneroLwsSpend
{
    /// <summary>
    /// XMR possibly being spent.
    /// </summary>
    [JsonPropertyName("amount")] public string Amount { get; set; } = "";
    /// <summary>
    /// Bytes of the key image.
    /// </summary>
    [JsonPropertyName("key_image")] public string KeyImage { get; set; } = "";
    /// <summary>
    /// Bytes of the tx public key.
    /// </summary>
    [JsonPropertyName("tx_pub_key")] public string TxPubKey { get; set; } = "";
    /// <summary>
    /// Index of source output.
    /// </summary>
    /// <remarks>
    /// <c>OutIndex</c> is a zero-based offset from the original received output.
    /// The variable within the monero codebase is the <c>vout</c> array, this is the index within that.
    /// It is needed for correct computation of the <c>KeyImage</c>. 
    /// </remarks>
    [JsonPropertyName("out_index")] public int OutIndex { get; set; } = 0;
    /// <summary>
    /// Mixin of the spend. Does not include the real spend - this is the number of dummy inputs.
    /// </summary>
    [JsonPropertyName("mixin")] public int Mixin { get; set; } = 0;
    /// <summary>
    /// Address data of the sender.
    /// </summary>
    [JsonPropertyName("sender")] public MoneroLwsAddressMeta Sender { get; set; } = new();
}
