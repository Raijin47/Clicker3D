using System;

public static class SizeUtilities
{
    private static readonly string[] _sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    public static string SizeSuffix(long value, int decimalPlaces = 1)
    {
        if (decimalPlaces < 0 || value <= 0)
        {
            return "0.0 KB";
        }

        var mag = (int)Math.Log(value, 1024);
        var adjustedSize = (decimal)value / (1L << (mag * 10));

        if (Math.Round(adjustedSize, decimalPlaces) < 1000) return $"{adjustedSize:n} {_sizeSuffixes[mag]}";
        mag += 1;
        adjustedSize /= 1024;

        return $"{adjustedSize:n} {_sizeSuffixes[mag]}";
    }
}