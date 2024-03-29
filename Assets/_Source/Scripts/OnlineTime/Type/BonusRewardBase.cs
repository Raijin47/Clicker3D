using UnityEngine;

public abstract class BonusRewardBase : BonusBase
{
    [SerializeField] protected double _baseReward;

    protected abstract double GetRewardValue();
    protected abstract void GetReward();

    protected override int GetTime()
    {
        return _baseTime;
    }

    protected override void UpdateBonusValue()
    {
        _textValue.text = ConvertNumber.Convert(GetRewardValue());
    }

    public override void Reward()
    {
        base.Reward();
        GetReward();
    }
}