using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

public class MoneroLwsAccount
{
    /// <summary>
    /// Account primary address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = "";

    /// <summary>
    /// Account scan height.
    /// </summary>
    [JsonPropertyName("scan_height")]
    public long ScanHeight { get; set; } = 0;

    /// <summary>
    /// Account start height.
    /// </summary>
    [JsonPropertyName("start_height")]
    public long StartHeight { get; set; } = 0;

    /// <summary>
    /// Account last access time.
    /// </summary>
    [JsonPropertyName("access_time")]
    public long AccessTime { get; set; } = 0;
}