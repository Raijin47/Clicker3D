using System;

public static class ConvertNumber
{
    private static string[] _typeValue = new[]
    {
        "", "k", "m", "A", "B", "C", "D", "E", "F", 
        "G", "H", "I", "J", "K", "L", "M", "N",
        "O", "P", "Q", "R", "S", "T", "U", "V",
        "W", "X", "Y", "Z",

        "AA", "AB", "AC", "AD", "AE", "AF",
        "AG", "AH", "AI", "AJ", "AK", "AL",
        "AM", "AN", "AO", "AP", "AQ", "AR",
        "AS", "AT", "AU", "AV", "AW", "AX",
        "AY", "AZ",

        "BA", "BB", "BC", "BD", "BE", "BF",
        "BG", "BH", "BI", "BJ", "BK", "BL",
        "BM", "BN", "BO", "BP", "BQ", "BR",
        "BS", "BT", "BU", "BV", "BW", "BX",
        "BY", "BZ",

        "CA", "CB", "CC", "CD", "CE", "CF",
        "CG", "CH", "CI", "CJ", "CK", "CL",
        "CM", "CN", "CO", "CP", "CQ", "CR",
        "CS", "CT", "CU", "CV", "CW", "CX",
        "CY", "CZ",

        "DA", "DB", "DC", "DD", "DE", "DF", "DG",
        "DK"
    };

    public static string Convert(double digit)
    {
        int indexer = 0;
        while (indexer + 1 < _typeValue.Length && digit >= 1000d)
        {
            digit /= 1000d;
            indexer++;
        }

        digit = Math.Round(digit, 2);
        return $"{digit}{_typeValue[indexer]}";
    }
}