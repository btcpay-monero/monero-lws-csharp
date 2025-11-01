namespace Monero.Lws.Common;

/// <summary>
/// Model a LWS API Exception.
/// </summary>
/// <param name="code">Error code.</param>
/// <param name="message">Error message.</param>
public class MoneroLwsApiException(int code, string message) : Exception(message)
{
    public readonly int Code = code;
}
