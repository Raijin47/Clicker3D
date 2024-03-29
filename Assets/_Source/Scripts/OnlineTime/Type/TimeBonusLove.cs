public class TimeBonusLove : TimeBonusBase
{
    protected override void Execute()
    {
        Modifier.TimeLoveBoost = _isActive ? Modifier.ModifireBonusAds : 1;
    } 
}