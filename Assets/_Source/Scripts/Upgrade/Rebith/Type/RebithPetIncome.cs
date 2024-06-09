public class RebithPetIncome : RebithIncreaseScaleBase
{
    protected override void Execute()
    {
        Modifier.PetIncomeModifier = _currentValue;
    }
}