using System.Text.Json.Serialization;

namespace Monero.Lws.Request;

public class MoneroLwsAdminParams
{
    /// <summary>
    /// Request type to accept or reject (<c>import</c> or <c>create</c>).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("type")] public string RequestType { get; set; } = "";

    /// <summary>
    /// List of addresses to <c>import</c>, <c>create</c> or <c>rescan</c>.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("addresses")] public List<string>? Addresses { get; set; } = null;

    /// <summary>
    /// Height from which to rescan <c>Addresses</c>.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("height")] public long? Height { get; set; } = null;
    
    /// <summary>
    /// Account primary address to add.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("address")] public string Address { get; set; } = "";

    /// <summary>
    /// Account private view key to add.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("key")] public string Key { get; set; } = "";

    /// <summary>
    /// New account status to apply to <c>Addresses</c>. Can be <c>active</c>, <c>hidden</c> or <c>inactive</c>.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("status")] public string Status { get; set; } = "";
    
    /// <summary>
    /// Address public view as hex.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("view_public_hex")] public string ViewPublicHex { get; set; } = "";

    /// <summary>
    /// Address public spend as hex.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("spend_public_hex")] public string SpendPublicHex { get; set; } = "";

    /// <summary>
    /// Address view key as hex.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("view_key_hex")] public string ViewKeyHex { get; set; } = "";

}
