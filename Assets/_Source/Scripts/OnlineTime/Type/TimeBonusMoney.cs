public class TimeBonusMoney : TimeBonusBase
{
    protected override void Execute()
    {
        Modifier.TimeMoneyBoost = _isActive ? Modifier.ModifireBonusAds : 1;
    }
}