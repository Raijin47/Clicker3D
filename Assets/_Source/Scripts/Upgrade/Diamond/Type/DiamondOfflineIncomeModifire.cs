public class DiamondOfflineIncomeModifire : DiamondLimited
{
    protected override void Execute()
    {
        Modifier.OfflineIncomeModifire = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        _effectText.text = _currentValue + "%";
    }

    protected override void UpdateTextProcess()
    {
        _effectText.text = _currentValue + "% > " + _nextValue + "%";
    }
}