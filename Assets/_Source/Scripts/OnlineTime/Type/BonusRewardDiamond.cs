public class BonusRewardDiamond : BonusRewardBase
{
    protected override void GetReward()
    {
        Locator.Instance.Wallet.Diamonds += GetRewardValue();
    }

    protected override double GetRewardValue()
    {
        return _baseReward * Modifier.ModifireBonusAds;
    }
}