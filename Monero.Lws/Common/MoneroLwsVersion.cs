using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Models Monero LWS Server version.
/// </summary>
public class MoneroLwsVersion
{
    [JsonPropertyName("server_type")] public string ServerType { get; set; } = "";
    [JsonPropertyName("server_version")] public string ServerVersion { get; set; } = "";
    [JsonPropertyName("last_git_commit_hash")] public string LastGitCommitHash { get; set; } = "";
    [JsonPropertyName("last_git_commit_date")] public string LastGitCommitDate { get; set; } = "";
    [JsonPropertyName("git_branch_name")] public string GitBranchName { get; set; } = "";
    [JsonPropertyName("monero_version_full")] public string MoneroVersionFull { get; set; } = "";
    [JsonPropertyName("blockchain_height")] public long BlockchainHeight { get; set; } = 0;
    [JsonPropertyName("api")] public long Api { get; set; } = 0;
    [JsonPropertyName("max_subaddresses")] public long MaxSubaddresses { get; set; } = 0;
    [JsonPropertyName("network_type")] public string NetworkType { get; set; } = "";
    [JsonPropertyName("testnet")] public bool Testnet { get; set; } = false;
}