public static class TextUtility
{
    public const string MoreSign = " > ";
    public const string Percent = "%";
    public const string PercentAndMore = "% > ";
    public const string Multiply = "x";
    public const string MoreAndMultiply = " > x";

    public const string PetGrade = "PetGrade";
    public const string JobGrade = "JobGrade";
    public const string ImprovementPetName = "ImprovementPetName{0}";
    public const string ImprovementPetDes0 = "ImprovementPetDescriptionGrade0{0}{1}";
    public const string ImprovementPetDes1 = "ImprovementPetDescriptionGrade1{0}{1}";
    public const string ImprovementPetDesName = "ImprovementPetDesName";

    public static string GetColorText(string text)
    {
        return $"<color=orange>{text}</color>";
    }
}