public class TimeBonusMoney : TimeBonusBase
{
    protected override void Execute()
    {
        Modifier.TimeMoneyBoost = _isActive ? Modifier.AdsBoostMoney : 1;
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        _textValue.text = TextUtility.GetBlackText(TextUtility.Multiply + Modifier.AdsBoostMoney + TextUtility.GoldImg);
    }
}