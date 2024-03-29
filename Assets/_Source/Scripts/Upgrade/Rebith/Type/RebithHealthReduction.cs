public class RebithHealthReduction : RebithLimited
{
    protected override void Execute()
    {
        Modifier.HealthReductionModifier = _currentValue;
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