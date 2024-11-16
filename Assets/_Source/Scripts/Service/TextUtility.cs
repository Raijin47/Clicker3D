public static class TextUtility
{
    public const string MoreSign = " <sprite=4> ";
    public const string Percent = "%";
    public const string PercentAndMore = "% <sprite=4> ";
    public const string Multiply = "x";
    public const string MoreAndMultiply = " <sprite=4> x";

    public const string LoveImg = "<sprite=0>";
    public const string GoldImg = "<sprite=1>";
    public const string DiamondImg = "<sprite=2>";
    public const string PrestigeImg = "<sprite=3>";

    public const string Explore = "Explore";
    public const string Buy = "Buy";
    public const string Improve = "Improve";
    public const string Max = "Max";
    public const string Traininig = "Training";

    public const string Grade = "Grade";
    public const string ImprovementPetName = "ImprovementPetName{0}";
    public const string ImprovementPetDes0 = "ImprovementPetDescriptionGrade0{0}{1}";
    public const string ImprovementPetDes1 = "ImprovementPetDescriptionGrade1{0}{1}";
    public const string ImprovementPetDesName = "ImprovementPetDesName";
    public const string ImprovementJobName = "ImprovementJobName";
    public const string ImprovementJobDesName = "ImprovementJobDesName";
    public const string ImprovementJobDes = "ImprovementJobDescriptionGrade{0}{1}";

    public static string GetColorText(string text)
    {
        return $"<color=green><b>{text}</b></color>";
    }

    public static string GetWhiteText(string text)
    {
        return $"<color=#FFFFC8>{text}</color>";
    }

    public static string GetBlackText(string text)
    {
        return $"<color=#212121>{text}</color>";
    }
}