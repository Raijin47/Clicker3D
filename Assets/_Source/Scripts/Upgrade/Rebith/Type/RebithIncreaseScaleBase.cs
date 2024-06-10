using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class RebithIncreaseScaleBase : RebithBase
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private double _degreeIncreaseValue;
    private const double _increaseEveryLevel = 10;
    private const double _increasePercent = 2;

    protected override void UpdateTextMax()
    {
        _effectText.text = ConvertNumber.Convert(_currentValue) + TextUtility.Percent;
        UpdateScale();
    }

    protected override void UpdateTextProcess()
    {
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        _effectText.text = ConvertNumber.Convert(_currentValue) + TextUtility.PercentAndMore + ConvertNumber.Convert(_nextValue) + TextUtility.Percent;
        UpdateScale();
    }

    protected override double CalculateUpgradeValue()
    {
        return base.CalculateUpgradeValue() * Math.Pow(_increasePercent, Math.Floor(Level / _increaseEveryLevel));
    }

    protected override double CalculateUpgradeNext()
    {
        return base.CalculateUpgradeNext() * Math.Pow(_increasePercent, Math.Floor((Level + 1) / _increaseEveryLevel));
    }

    private void UpdateScale()
    {
        double a = Level / _increaseEveryLevel;

        float b = (float)(a - Math.Floor(a));

        _fillImage.fillAmount = b;
    }
}