using System;

public static class IncreaseValue
{
    public static double Calculate(double level, double baseValue, double degree)
    {
        level += 1;

        double value = baseValue * Math.Pow(level, degree);
        value = Math.Round(value);
        return value;
    }
}