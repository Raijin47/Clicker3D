using System;

public static class IncreaseValue
{
    public static double Calculate(double level, double baseValue, double multiply)
    {
        double value = baseValue * Math.Pow(multiply, level);
        return Math.Round(value);
    }

    public static double CalculateDegree(double level, double baseValue, double degree)
    {
        double Level = level + 1;
        double value = baseValue * Math.Pow(Level, degree);
        return Math.Round(value);
    }

    public static double CalculateConstant(double level, double constant, double power)
    {
        double value = level / Math.Pow(constant, power);
        return Math.Round(value);
    }
}