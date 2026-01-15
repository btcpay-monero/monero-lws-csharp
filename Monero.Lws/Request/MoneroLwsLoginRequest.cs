using System.Text.Json.Serialization;

using Monero.Lws.Common;

namespace Monero.Lws.Request;

/// <summary>
/// Class <c>MoneroLwsLoginRequest</c> models a request for account check or creation.
/// The view key bytes are required even if an account is not being created, to prevent metadata leakage.
/// </summary>
public class MoneroLwsLoginRequest : MoneroLwsWalletRequest
{
    /// <summary>
    /// Try to create a new account.
    /// </summary>
    [JsonPropertyName("create_account")]
    public bool CreateAccount { get; set; } = true;

    /// <summary>
    /// Indicate that the address is new.
    /// </summary>
    [JsonPropertyName("generated_locally")]
    public bool GeneratedLocally { get; set; } = true;

    /// <summary>
    /// Desired lookahead for new account.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("lookahead")]
    public MoneroLwsAccountLookahead? Lookahead { get; set; } = null;
}