public class DiamondTimeBoostAds : DiamondLimited
{
    protected override void Execute()
    {
        Modifier.BonusDurationTime = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        _effectText.text = "x" + _currentValue;
    }

    protected override void UpdateTextProcess()
    {
        _effectText.text = "x" + _currentValue + " > x" + _nextValue;
    }
}