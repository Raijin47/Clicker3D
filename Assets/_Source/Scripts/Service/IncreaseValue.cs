using System;

public static class IncreaseValue
{
    public static double Calculate(double level, double baseValue, double degree)
    {
        double value = baseValue * Math.Pow(degree, level);
        value = Math.Round(value);
        return value;
    }
}