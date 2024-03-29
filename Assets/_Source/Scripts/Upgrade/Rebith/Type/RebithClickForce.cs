public class RebithClickForce : RebithBase
{
    protected override void Execute()
    {
        Modifier.PrestigeClickForce = _currentValue;
    }

    protected override void UpdateUI()
    {
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        _effectText.text = ConvertNumber.Convert(_currentValue) + "% > " + ConvertNumber.Convert(_nextValue) + "%";
    }
}