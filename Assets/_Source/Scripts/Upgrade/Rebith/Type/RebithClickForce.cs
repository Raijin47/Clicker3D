public class RebithClickForce : RebithBase
{
    [UnityEngine.SerializeField] private double _degreeIncreaseValue;

    protected override void Execute()
    {
        Modifier.PrestigeClickForce = _currentValue;
    }

    protected override void UpdateUI()
    {
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        _effectText.text = ConvertNumber.Convert(_currentValue) + "% > " + ConvertNumber.Convert(_nextValue) + "%";
    }

    protected override double CalculateUpgradeValue()
    {
        return IncreaseValue.Calculate(Level, _baseValue, _degreeIncreaseValue);
    }

    protected override double CalculateUpgradeNext()
    {
        return IncreaseValue.Calculate(Level + 1, _baseValue, _degreeIncreaseValue);
    }
}