using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Response;

public class MoneroLwsListRequestsResponse
{
    /// <summary>
    /// Account <c>create</c> requests.
    /// </summary>
    [JsonPropertyName("create")]
    public List<MoneroLwsAccount> Create { get; set; } = [];

    /// <summary>
    /// Account <c>import</c> requests.
    /// </summary>
    [JsonPropertyName("import")]
    public List<MoneroLwsAccount> Import { get; set; } = [];
}