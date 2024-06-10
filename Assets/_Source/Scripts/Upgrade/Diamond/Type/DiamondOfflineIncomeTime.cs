using System;

public class DiamondOfflineIncomeTime : DiamondBase
{
    protected override void Execute()
    {
        Modifier.OfflineIncomeTime = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)_currentValue);
        _effectText.text = dateTimeOffset.ToString("HH:mm:ss");
    }

    protected override void UpdateTextProcess()
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)_currentValue);
        DateTimeOffset nextTimeOffset = DateTimeOffset.FromUnixTimeSeconds((int)_nextValue);
        _effectText.text = dateTimeOffset.ToString("HH:mm:ss") + TextUtility.MoreSign + nextTimeOffset.ToString("HH:mm:ss");
    }
}