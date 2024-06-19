public class DiamondOfflineIncomeModifire : DiamondBase
{
    protected override void Execute()
    {
        Modifier.OfflineIncomeModifire = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        _effectText.text = _currentValue + TextUtility.Percent;
    }

    protected override void UpdateTextProcess()
    {
        _effectText.text = _currentValue + TextUtility.PercentAndMore + TextUtility.GetColorText(_nextValue + TextUtility.Percent);
    }
}