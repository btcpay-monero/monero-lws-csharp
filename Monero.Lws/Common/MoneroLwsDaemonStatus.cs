using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

public class MoneroLwsDaemonStatus
{
    /// <summary>
    /// Outgoing connections from daemon.
    /// </summary>
    [JsonPropertyName("outgoing_connections_count")]
    public long OutgoingConnectionsCount { get; set; } = 0;

    /// <summary>
    /// Incoming connections to daemon.
    /// </summary>
    [JsonPropertyName("incoming_connections_count")]
    public long IncomingConnectionsCount { get; set; } = 0;

    /// <summary>
    /// Daemon height.
    /// </summary>
    [JsonPropertyName("height")]
    public long Height { get; set; } = 0;

    /// <summary>
    /// Target height.
    /// </summary>
    [JsonPropertyName("target_height")]
    public long TargetHeight { get; set; } = 0;

    /// <summary>
    /// Network type.
    /// </summary>
    [JsonPropertyName("network")]
    public string Network { get; set; } = "";

    /// <summary>
    /// Daemon status.
    /// </summary>
    [JsonPropertyName("state")]
    public string State { get; set; } = "";
}