using YG;
using System;

public abstract class RebithBase : UpgradeBase
{
    private const double _priceModifire = 0.05d;

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
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Rebith >= _currentPrice;
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
        return Math.Round(_baseUpgradePrice + Level * (_baseUpgradePrice + _priceModifire * Level));
        //return Math.Round(_baseUpgradePrice + Level * (_baseUpgradePrice * 1.00001d * Level));
        //return System.Math.Round(1 + Level * (_baseUpgradePrice +
        //    _baseUpgradePrice * 0.005d * Level));

    }
}