using UnityEngine;

public class BonusRewardMoney : BonusRewardBase
{
    [SerializeField] private double _modifierRewardTime;

    public override void Init()
    {
        base.Init();
        GlobalEvent.OnChangeJobIncome.AddListener(UpdateBonusValue);
        GlobalEvent.OnIncreaseClick.AddListener(UpdateBonusValue);
    }

    protected override void GetReward()
    {
        Locator.Instance.Wallet.Money += GetRewardValue();
    }

    protected override double GetRewardValue()
    {
        return (Locator.Instance.Click.ClickIncome + Locator.Instance.JobsManager.CurrentIncome) * _modifierRewardTime * Modifier.ModifireBonusAds;
    }
}