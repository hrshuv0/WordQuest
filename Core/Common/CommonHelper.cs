namespace Core.Common;

public static class CommonHelper
{
    public static string TrimString(this string? str)
    {
        return string.IsNullOrWhiteSpace(str) ? string.Empty :  str.Trim();
    }
    
}