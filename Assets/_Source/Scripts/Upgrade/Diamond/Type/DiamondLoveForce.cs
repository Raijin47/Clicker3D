public class DiamondLoveForce : DiamondBase
{
    protected override void Execute()
    {
        Modifier.DiamondIncome = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        _effectText.text = _currentValue + TextUtility.Percent;
    }

    protected override void UpdateTextProcess()
    {
        _effectText.text = _currentValue + TextUtility.PercentAndMore + _nextValue + TextUtility.Percent;
    }
}