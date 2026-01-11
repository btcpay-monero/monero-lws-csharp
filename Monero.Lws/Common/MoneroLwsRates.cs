using System.Text.Json.Serialization;

namespace Monero.Lws.Common;

/// <summary>
/// Class <c>MoneroLwsRate</c> models the current exchange rates.
/// </summary>
public class MoneroLwsRates
{
    /// <summary>
    /// AUD/XMR exchange rate
    /// </summary>
    [JsonPropertyName("AUD")]
    public float? Aud { get; set; } = null;

    /// <summary>
    /// BRL/XMR exchange rate
    /// </summary>
    [JsonPropertyName("BRL")]
    public float? Brl { get; set; } = null;

    /// <summary>
    /// BTC/XMR exchange rate
    /// </summary>
    [JsonPropertyName("BTC")]
    public float? Btc { get; set; } = null;

    /// <summary>
    /// CAD/XMR exchange rate
    /// </summary>
    [JsonPropertyName("CAD")]
    public float? Cad { get; set; } = null;

    /// <summary>
    /// CHF/XMR exchange rate
    /// </summary>
    [JsonPropertyName("CHF")]
    public float? Chf { get; set; } = null;

    /// <summary>
    /// CNY/XMR exchange rate
    /// </summary>
    [JsonPropertyName("CNY")]
    public float? Cny { get; set; } = null;

    /// <summary>
    /// EUR/XMR exchange rate
    /// </summary>
    [JsonPropertyName("EUR")]
    public float? Eur { get; set; } = null;

    /// <summary>
    /// GBP/XMR exchange rate
    /// </summary>
    [JsonPropertyName("GBP")]
    public float? Gbp { get; set; } = null;

    /// <summary>
    /// HKD/XMR exchange rate
    /// </summary>
    [JsonPropertyName("HKD")]
    public float? Hkd { get; set; } = null;

    /// <summary>
    /// INR/XMR exchange rate
    /// </summary>
    [JsonPropertyName("INR")]
    public float? Inr { get; set; } = null;

    /// <summary>
    /// JPY/XMR exchange rate
    /// </summary>
    [JsonPropertyName("JPY")]
    public float? Jpy { get; set; } = null;

    /// <summary>
    /// KRW/XMR exchange rate
    /// </summary>
    [JsonPropertyName("KRW")]
    public float? Krw { get; set; } = null;

    /// <summary>
    /// MXN/XMR exchange rate
    /// </summary>
    [JsonPropertyName("MXN")]
    public float? Mxn { get; set; } = null;

    /// <summary>
    /// NDK/XMR exchange rate
    /// </summary>
    [JsonPropertyName("NOK")]
    public float? Nok { get; set; } = null;

    /// <summary>
    /// NZD/XMR exchange rate
    /// </summary>
    [JsonPropertyName("NZD")]
    public float? Nzd { get; set; } = null;

    /// <summary>
    /// SEK/XMR exchange rate
    /// </summary>
    [JsonPropertyName("SEK")]
    public float? Sek { get; set; } = null;

    /// <summary>
    /// SGD/XMR exchange rate
    /// </summary>
    [JsonPropertyName("SGD")]
    public float? Sgd { get; set; } = null;

    /// <summary>
    /// TRY/XMR exchange rate
    /// </summary>
    [JsonPropertyName("TRY")]
    public float? Try { get; set; } = null;

    /// <summary>
    /// USD/XMR exchange rate
    /// </summary>
    [JsonPropertyName("USD")]
    public float? Usd { get; set; } = null;

    /// <summary>
    /// RUB/XMR exchange rate
    /// </summary>
    [JsonPropertyName("RUB")]
    public float? Rub { get; set; } = null;

    /// <summary>
    /// ZAR/XMR exchange rate
    /// </summary>
    [JsonPropertyName("ZAR")]
    public float? Zar { get; set; } = null;
}