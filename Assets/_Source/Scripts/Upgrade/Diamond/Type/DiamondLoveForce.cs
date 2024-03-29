public class DiamondLoveForce : DiamondLimited
{
    protected override void Execute()
    {
        Modifier.DiamondIncome = _currentValue;
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