using System.Text.Json.Serialization;

namespace Monero.Lws.Response;

/// <summary>
/// Class <c>MoneroLwsImportRequestResponse</c> models an account scan request from the genesis block.
/// </summary>
public class MoneroLwsImportWalletResponse : MoneroLwsStatusResponse
{
    /// <summary>
    /// Payment location. Null if the client does not need to send XMR to complete the request.
    /// </summary>
    [JsonPropertyName("payment_address")] public string? PaymentAddress { get; set; } = null;
    /// <summary>
    /// Bytes for payment_id tx field. Null if the client does not need to send XMR to complete the request.
    /// </summary>
    [JsonPropertyName("payment_id")] public string? PaymentId { get; set; } = null;
    /// <summary>
    /// Fee required to complete request. Null if the client does not need to send XMR to complete the request.
    /// </summary>
    [JsonPropertyName("import_fee")] public string? ImportFee { get; set; } = null;
    /// <summary>
    /// New or existing request.
    /// </summary>
    [JsonPropertyName("new_request")] public bool NewRequest { get; set; } = false;
    /// <summary>
    /// Indicates success.
    /// </summary>
    [JsonPropertyName("request_fulfilled")] public bool RequestFulfilled { get; set; } = false;
}
