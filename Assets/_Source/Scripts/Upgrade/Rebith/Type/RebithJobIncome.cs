public class RebithJobIncome : RebithIncreaseScaleBase
{
    protected override void Execute()
    {
        Modifier.JobIncomeModifier = _currentValue;
    }
}