using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Request;

/// <summary>
/// Upsert subaddresses at the specified major and minor indexes. This endpoint is idempotent.
/// </summary>
public class MoneroLwsUpsertSubaddrsRequest : MoneroLwsWalletRequest
{
    /// <summary>
    /// Subaddresses to upsert.
    /// </summary>
    [JsonPropertyName("subaddrs")]
    public List<MoneroLwsSubaddrsEntry> Subaddrs { get; set; } = [];

    /// <summary>
    /// Whether to include all subaddresses in response.
    /// </summary>
    [JsonPropertyName("get_all")]
    public bool GetAll { get; set; } = false;
}