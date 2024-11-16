public class TimeBonusLove : TimeBonusBase
{
    protected override void Execute()
    {
        Modifier.TimeLoveBoost = _isActive ? Modifier.AdsBoostLove : 1;
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _textValue.text = TextUtility.GetBlackText(TextUtility.Multiply + Modifier.AdsBoostLove + TextUtility.LoveImg);
    }
}