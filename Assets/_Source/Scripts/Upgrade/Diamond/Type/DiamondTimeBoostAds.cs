using System;

public class DiamondTimeBoostAds : DiamondBase
{
    protected override void Execute()
    {
        Modifier.BoostDurationTime = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)_currentValue);

        _effectText.text = dateTimeOffset.ToString("mm:ss");
    }

    protected override void UpdateTextProcess()
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)_currentValue);
        DateTimeOffset nextTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)_nextValue);
        _effectText.text = dateTimeOffset.ToString("mm:ss") + TextUtility.MoreSign + TextUtility.GetColorText(nextTimeOffset.ToString("mm:ss"));
    }
}