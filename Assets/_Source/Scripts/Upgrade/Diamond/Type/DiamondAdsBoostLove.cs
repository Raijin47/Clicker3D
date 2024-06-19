public class DiamondAdsBoostLove : DiamondIncreaseScaleBase
{
    protected override void Execute()
    {
        Modifier.AdsBoostLove = _currentValue;
    }
}