public class DiamondReductionLostDays : DiamondBase
{
    protected override void Execute()
    {
        Modifier.ReductionLostDays = _currentValue;
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