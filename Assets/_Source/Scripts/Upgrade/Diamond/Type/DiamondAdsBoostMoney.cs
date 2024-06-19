public class DiamondAdsBoostMoney : DiamondIncreaseScaleBase
{
    protected override void Execute()
    {
        Modifier.AdsBoostMoney = _currentValue;
    }
}