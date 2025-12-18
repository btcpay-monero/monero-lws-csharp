namespace Monero.Lws.IntegrationTests.Utils;

internal static class TestUtils
{
    public static readonly bool TestsInContainer = GetDefaultEnv("TESTS_INCONTAINER", "false") == "true";
    public static readonly Uri LwsServiceUri = new(GetDefaultEnv("XMR_LWS_URI", "http://127.0.0.1:8443"));
    public const string Username = "";
    public const string Password = "";
    public const string Address = "42EhKmBx6pAPYhX4QCHKBPRw8dgc3VVVdA7g2dxr5wz21crqvPUkwPTde64Xac5uawQeFbh6K7PD4YLqiX1VTP5jUH7gZez";
    public const string ViewKey = "41f55a92b942681e35bf7bb64f71142729039bd8e606a4f4218c543065c15c05";

    private static MoneroLwsService? _lwsService = null;
    
    public static MoneroLwsService GetLwsService()
    {
        if (_lwsService == null)
        {
            _lwsService = new MoneroLwsService(LwsServiceUri, "lws", Username, Password);
        }

        return _lwsService;
    }
    
    private static string GetDefaultEnv(string key, string defaultValue)
    {
        string? currentValue = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrEmpty(currentValue) ? defaultValue : currentValue;
    }
    
}