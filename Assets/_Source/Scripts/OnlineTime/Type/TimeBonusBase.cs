public abstract class TimeBonusBase : BonusBase
{
    protected bool _isActive;

    protected abstract void Execute();

    protected override int GetTime()
    {
        return (int)(Modifier.BoostDurationTime);
    }

    protected override void ShowInfo()
    {
        base.ShowInfo();
        _isActive = false;
        UpdateUI();
        Execute();
    }

    protected override void ShowTimer()
    {
        base.ShowTimer();
        _isActive = true;
        Execute();
    }

    protected override void UpdateBonusValue()
    {
        UpdateUI();
        Execute();
    }

    protected virtual void UpdateUI()
    {
        if (_timeBonusService.IsActiveTimer) return;
    }
}