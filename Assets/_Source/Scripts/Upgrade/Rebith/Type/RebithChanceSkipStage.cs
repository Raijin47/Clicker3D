public class RebithChanceSkipStage : RebithBase
{
    protected override void Execute()
    {
        Modifier.ChanceSkipStage = _currentValue;
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