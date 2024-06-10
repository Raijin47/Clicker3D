public class DiamondModifireBoostAds : DiamondBase
{
    protected override void Execute()
    {
        Modifier.ModifireBonusAds = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        _effectText.text = TextUtility.Multiply + _currentValue;
    }

    protected override void UpdateTextProcess()
    {
        _effectText.text = TextUtility.Multiply + _currentValue + TextUtility.MoreAndMultiply + _nextValue;
    }
}