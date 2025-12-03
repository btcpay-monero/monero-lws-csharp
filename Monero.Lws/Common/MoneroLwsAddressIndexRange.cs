using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monero.Lws.Common;
/// <summary>
/// Inclusive minor address index range.
/// </summary>
public class MoneroLwsAddressIndexRange
{
    /// <summary>
    /// Inclusive lower bound.
    /// </summary>
    public long LowerBound { get; set; }
    /// <summary>
    /// Inclusive upper bound.
    /// </summary>
    public long UpperBound { get; set; }

    public MoneroLwsAddressIndexRange(long lowerBound, long upperBound)
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
    }
    
    [JsonConstructor]
    public MoneroLwsAddressIndexRange(JsonElement json)
    {
        if (json.ValueKind != JsonValueKind.Array)
            throw new JsonException("index_range must be an array");

        if (json.GetArrayLength() != 2)
            throw new JsonException("index_range must contain exactly two numbers");

        var arr = json.EnumerateArray();
        arr.MoveNext();
        LowerBound = arr.Current.GetInt64();

        arr.MoveNext();
        UpperBound = arr.Current.GetInt64();
    }
}