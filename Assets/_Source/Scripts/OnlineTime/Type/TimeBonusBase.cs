using Assets.SimpleLocalization;
using UnityEngine;

public abstract class TimeBonusBase : BonusBase
{
    //[SerializeField] private LocalizedDynamic _textTime;
    private readonly string _x = "x";
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
        return (int)(Modifier.BonusDurationTime * _baseTime);
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
        _textValue.text = _x + Modifier.ModifireBonusAds;
        Execute();
    }

    protected void UpdateTimeUI()
    {
        //_textTime.SetValue(GetTime().ToString());
    }
}