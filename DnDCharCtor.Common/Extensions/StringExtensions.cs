namespace DnDCharCtor.Common.Extensions;

public static class StringExtensions
{
    public static int? ToInt(this string value)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }
        return null;
    }
}

