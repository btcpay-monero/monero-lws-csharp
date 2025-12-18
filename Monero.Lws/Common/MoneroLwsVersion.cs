using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Models Monero LWS Server version.
/// </summary>
public class MoneroLwsVersion
{
    /// <summary>
    /// Name of server implementation.
    /// </summary>
    [JsonPropertyName("server_type")] public string ServerType { get; set; } = "";
    /// <summary>
    /// Version of server implementation.
    /// </summary>
    [JsonPropertyName("server_version")] public string ServerVersion { get; set; } = "";
    /// <summary>
    /// A hash (of server determined length) corresponding to commit hash.
    /// </summary>
    [JsonPropertyName("last_git_commit_hash")] public string LastGitCommitHash { get; set; } = "";
    /// <summary>
    /// Implementation defined commit date.
    /// </summary>
    [JsonPropertyName("last_git_commit_date")] public string LastGitCommitDate { get; set; } = "";
    /// <summary>
    /// Implementation defined branch name.
    /// </summary>
    [JsonPropertyName("git_branch_name")] public string GitBranchName { get; set; } = "";
    /// <summary>
    /// Implementation defined version string.
    /// </summary>
    [JsonPropertyName("monero_version_full")] public string MoneroVersionFull { get; set; } = "";
    /// <summary>
    /// Current blockchain height (as processed by LWS server).
    /// </summary>
    [JsonPropertyName("blockchain_height")] public long BlockchainHeight { get; set; } = 0;
    /// <summary>
    /// Unique number per schema Implementation.
    /// </summary>
    [JsonPropertyName("api")] public long Api { get; set; } = 0;
    /// <summary>
    /// Max subaddresses allowed by server.
    /// </summary>
    [JsonPropertyName("max_subaddresses")] public long MaxSubaddresses { get; set; } = 0;
    /// <summary>
    /// Name of network type.
    /// </summary>
    [JsonPropertyName("network_type")] public string NetworkType { get; set; } = "";
    /// <summary>
    /// True if running on testnet.
    /// </summary>
    [JsonPropertyName("testnet")] public bool Testnet { get; set; } = false;
}