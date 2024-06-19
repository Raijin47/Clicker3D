public abstract class TimeBonusBase : BonusBase
{
    protected bool _isActive;

    protected abstract void Execute();

    public override void Init()
    {
        UpdateTimeUI();
        base.Init();
        GlobalEvent.OnChangeBonusDurationTime.AddListener(UpdateTimeUI);
    }

    protected override int GetTime()
    {
        return (int)(Modifier.BoostDurationTime);
    }

    protected override void ShowInfo()
    {
        base.ShowInfo();
        _isActive = false;
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

    protected abstract void UpdateUI();


    protected void UpdateTimeUI()
    {
        //_textTime.SetValue(GetTime().ToString());
    }
}