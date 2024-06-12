using YG;

public abstract class RebithBase : UpgradeBase
{
    private const double _increaseDegreePrice = 1.3d;

    protected override void AddListener()
    {
        GlobalEvent.OnRebithChange.AddListener(CheckInteractableButton);
    }

    protected override int GetLevel()
    {
        int level = YandexGame.savesData.RebithLevel[_id];
        return level;
    }

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Rebith >= _currentPrice && Level != _maxLevel;
        return _isPurchaseAvailable;
    }

    protected override void ExecutePurchase()
    {
        Locator.Instance.Wallet.Rebith -= _currentPrice;
    }

    protected override void SetLevel()
    {
        YandexGame.savesData.RebithLevel[_id] = Level;
    }

    protected override void PlayParticle()
    {
        Locator.Instance.Particle.PrestigeTransform.position = _upgradeButton.transform.position;
        Locator.Instance.Particle.PrestigeParticle.Play();
    }

    protected override double CalculateUpgradePrice()
    {
        return IncreaseValue.CalculateDegree(Level, _baseUpgradePrice, _increaseDegreePrice);
    }
}