public class TimeBonusMoney : TimeBonusBase
{
    protected override void Execute()
    {
        Modifier.TimeMoneyBoost = _isActive ? Modifier.AdsBoostMoney : 1;
    }

    protected override void UpdateUI()
    {
        _textValue.text = TextUtility.Multiply + Modifier.AdsBoostMoney;
    }
}