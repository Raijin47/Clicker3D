public class DiamondTimeBoostAds : DiamondBase
{
    protected override void Execute()
    {
        Modifier.BonusDurationTime = _currentValue;
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