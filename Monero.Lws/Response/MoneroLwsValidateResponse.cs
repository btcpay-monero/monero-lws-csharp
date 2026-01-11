using System.Text.Json.Serialization;

namespace Monero.Lws.Response;

public class MoneroLwsValidateResponse
{
    public class MoneroValidationError
    {
        [JsonPropertyName("field")] public string Field { get; set; } = "";
        [JsonPropertyName("details")] public string Details { get; set; } = "";
    }

    /// <summary>
    /// Validated account address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = "";

    /// <summary>
    /// Validation error.
    /// </summary>
    [JsonPropertyName("error")]
    public MoneroValidationError? Error { get; set; } = null;
}