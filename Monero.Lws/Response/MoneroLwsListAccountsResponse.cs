using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Response;

public class MoneroLwsListAccountsResponse
{
    /// <summary>
    /// Active accounts.
    /// </summary>
    [JsonPropertyName("active")]
    public List<MoneroLwsAccount> Active { get; set; } = [];

    /// <summary>
    /// Invactive account.
    /// </summary>
    [JsonPropertyName("inactive")]
    public List<MoneroLwsAccount> Inactive { get; set; } = [];

    /// <summary>
    /// Deleted accounts.
    /// </summary>
    [JsonPropertyName("hidden")]
    public List<MoneroLwsAccount> Hidden { get; set; } = [];
}