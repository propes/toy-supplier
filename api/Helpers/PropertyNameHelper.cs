public static class PropertyNameHelper
{
    public static string ToCamelCase(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return char.ToLowerInvariant(str[0]) + (str.Length > 1 ? str[1..] : "");
    }
}
