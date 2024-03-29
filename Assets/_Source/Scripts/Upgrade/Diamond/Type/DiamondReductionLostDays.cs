public class DiamondReductionLostDays : DiamondLimited
{
    protected override void Execute()
    {
        Modifier.ReductionLostDays = _currentValue;
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