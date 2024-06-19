public class RebithCostReductionPurchase : RebithBase
{
    protected override void Execute()
    {
        Modifier.CostReductionPurchase = _currentValue;
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