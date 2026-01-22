using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monero.Lws.IntegrationTests.Utils;

internal class TestConfig
{
    [JsonPropertyName("wallets")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<WalletInfo> Wallets { get; set; } = [];

    private void Validate()
    {
        if (Wallets.Count == 0)
        {
            throw new Exception("No wallets found in configuration");
        }

        int i = 0;
        foreach (var wallet in Wallets)
        {
            ValidateWallet(wallet, i);
            i++;
        }
    }

    public static TestConfig Load()
    {
        var json = File.ReadAllText("settings.json");
        var config = JsonSerializer.Deserialize<TestConfig>(json);

        if (config == null)
        {
            throw new Exception("config is null");
        }

        config.Validate();

        return config;
    }

    private static void ValidateWallet(WalletInfo? info, int index)
    {
        if (info == null)
        {
            throw new Exception($"Found null configuration at wallet index {index}");
        }

        if (string.IsNullOrEmpty(info.PrimaryAddress))
        {
            throw new Exception($"Invalid primary address found at wallet index {index}");
        }

        if (string.IsNullOrEmpty(info.PublicViewKey))
        {
            throw new Exception($"Invalid public view key found at wallet index {index}");
        }

        if (string.IsNullOrEmpty(info.PublicSpendKey))
        {
            throw new Exception($"Invalid public spend key found at wallet index {index}");
        }

        if (string.IsNullOrEmpty(info.PrivateViewKey))
        {
            throw new Exception($"Invalid private view key found at wallet {index}");
        }
    }
}