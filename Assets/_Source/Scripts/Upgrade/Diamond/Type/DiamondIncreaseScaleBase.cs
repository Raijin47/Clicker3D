using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class DiamondIncreaseScaleBase : DiamondBase
{
    [SerializeField] private Image _fillImage;
    private const double _increaseEveryLevel = 10;
    private const double _increasePercent = 3;

    protected override void UpdateTextMax()
    {
        _effectText.text = ConvertNumber.Convert(_currentValue * 100) + TextUtility.Percent;
        UpdateScale();
    }

    protected override void UpdateTextProcess()
    {
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        _effectText.text = ConvertNumber.Convert(_currentValue * 100) + TextUtility.PercentAndMore + TextUtility.GetColorText(ConvertNumber.Convert(_nextValue * 100) + TextUtility.Percent);
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
