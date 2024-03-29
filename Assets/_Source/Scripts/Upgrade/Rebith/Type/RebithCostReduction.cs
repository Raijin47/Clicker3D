public class RebithCostReduction : RebithLimited
{
    protected override void Execute()
    {
        Modifier.CostReductionModifier = _currentValue;
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