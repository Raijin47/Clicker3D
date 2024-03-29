using System;

public class EnhancementForceClick : EnhancementBase
{
    protected override void Execute()
    {
        Modifier.EnhancementClickForce = _currentValue;
    }

    protected override void ResetLevel()
    {
        Level = 1;
        UpdateValue();
    }

    protected override void AddListener()
    {
        base.AddListener();
        GlobalEvent.OnIncreaseClick.AddListener(UpdateUI);
    }

    protected override void UpdateUI()
    {
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        _effectText.text = ConvertNumber.Convert(
            Math.Round(_currentValue * Modifier.PrestigeClickForce * Modifier.DiamondIncome * Modifier.ADsBoost)) 
            + " > " + ConvertNumber.Convert(
                Math.Round(_nextValue * Modifier.PrestigeClickForce * Modifier.DiamondIncome * Modifier.ADsBoost));
    }
}