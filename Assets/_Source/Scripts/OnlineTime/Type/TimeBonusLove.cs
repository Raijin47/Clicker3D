public class TimeBonusLove : TimeBonusBase
{
    protected override void Execute()
    {
        Modifier.TimeLoveBoost = _isActive ? Modifier.AdsBoostLove : 1;
    }

    protected override void UpdateUI()
    {
        _textValue.text = TextUtility.Multiply + Modifier.AdsBoostLove;
    }
}