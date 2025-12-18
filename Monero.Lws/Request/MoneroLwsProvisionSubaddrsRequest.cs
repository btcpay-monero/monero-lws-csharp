using System.Text.Json.Serialization;

namespace Monero.Lws.Request;
/// <summary>
/// Provision subaddresses at specified indexes. No two clients should ever receive
/// the same newly provisioned subaddresses when calling this endpoint; the server
/// should guarantee that newly provisioned subaddresses are fresh.
/// </summary>
public class MoneroLwsProvisionSubaddrsRequest() : MoneroLwsWalletRequest()
{
    /// <summary>
    /// Subaddress major index.
    /// </summary>
    [JsonPropertyName("maj_i")] public long MajIndex { get; set; } = 0;
    /// <summary>
    /// Subaddress minor index.
    /// </summary>
    [JsonPropertyName("min_i")] public long MinIndex { get; set; } = 0;
    /// <summary>
    /// Number of major subaddresses to provision.
    /// </summary>
    [JsonPropertyName("n_maj")] public long MajCount { get; set; } = 0;
    /// <summary>
    /// Number of minor subaddresses to provision.
    /// </summary>
    [JsonPropertyName("n_min")] public long MinCount { get; set; } = 0;
    /// <summary>
    /// Whether to include all subaddresses in response.
    /// </summary>
    [JsonPropertyName("get_all")] public bool GetAll { get; set; } = false;
}