public class RebithPetIncome : RebithBase
{
    protected override void Execute()
    {
        Modifier.PetIncomeModifier = _currentValue;
    }

    protected override void UpdateUI()
    {
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        _effectText.text = ConvertNumber.Convert(_currentValue) + "% > " + ConvertNumber.Convert(_nextValue) + "%";
    }
}