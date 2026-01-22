namespace Monero.Lws.IntegrationTests.Utils;

internal static class TestUtils
{
    public static readonly bool TestsInContainer = GetDefaultEnv("TESTS_INCONTAINER", "false") == "true";
    public static readonly Uri LwsServiceUri = new(GetDefaultEnv("XMR_LWS_URI", "http://127.0.0.1:8443"));
    public const string Username = "";
    public const string Password = "";
    public static readonly TestConfig Config = TestConfig.Load();

    private static MoneroLwsService? _lwsService = null;

    public static MoneroLwsService GetLwsService()
    {
        if (_lwsService == null)
        {
            _lwsService = new MoneroLwsService(LwsServiceUri, "lws", "admin", Username, Password);
        }

        return _lwsService;
    }

    private static string GetDefaultEnv(string key, string defaultValue)
    {
        string? currentValue = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrEmpty(currentValue) ? defaultValue : currentValue;
    }
}