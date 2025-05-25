namespace OneMonthFlow.Shared;

public static partial class DevCode
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T ToObject<T>(this string jsonStr)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(jsonStr,
                new JsonSerializerSettings { DateParseHandling = DateParseHandling.DateTimeOffset });
            return result!;
        }
        catch
        {
            return (T)Convert.ChangeType(jsonStr, typeof(T));
        }
    }

    public static bool IsNullOrEmpty(this object? str)
    {
        var result = true;
        result = str == null ||
                 string.IsNullOrEmpty(str.ToString()?.Trim()) ||
                 string.IsNullOrWhiteSpace(str.ToString()?.Trim());

        return result;
    }
}