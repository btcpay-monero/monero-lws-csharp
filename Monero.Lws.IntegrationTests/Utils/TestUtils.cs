namespace Monero.Lws.IntegrationTests.Utils;

internal static class TestUtils
{
    private static readonly MoneroLwsService? lwsService = null;

    public static readonly Uri LwsServiceUri = new(GetDefaultEnv("XMR_LWS_URI", "http://127.0.0.1:8443"));
    public const string Username = "";
    public const string Password = "";
    public static readonly TestConfig Config = TestConfig.Load();

    public static MoneroLwsService GetLwsService()
    {
        return lwsService ?? new MoneroLwsService(LwsServiceUri, "lws", "admin", Username, Password);
    }

    private static string GetDefaultEnv(string key, string defaultValue)
    {
        string? currentValue = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrEmpty(currentValue) ? defaultValue : currentValue;
    }
}