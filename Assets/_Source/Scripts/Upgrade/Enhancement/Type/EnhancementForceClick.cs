using System;

public class EnhancementForceClick : EnhancementBase
{
    [UnityEngine.SerializeField] private double _degreeIncreaseValue;

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

    protected override double CalculateUpgradeValue()
    {
        double value = IncreaseValue.Calculate(Level, _baseValue, _degreeIncreaseValue);
        return value;
    }

    protected override double CalculateUpgradeNext()
    {
        double value = IncreaseValue.Calculate(Level + 1, _baseValue, _degreeIncreaseValue);
        return value;
    }
}