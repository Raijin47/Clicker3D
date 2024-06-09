using UnityEngine;
using YG;

public abstract class DiamondBase : UpgradeBase
{
    [SerializeField] private double _fixedIncreasePrice;


    protected override void AddListener()
    {
        GlobalEvent.OnDiamondChange.AddListener(CheckInteractableButton);
    }

    protected override int GetLevel()
    {
        int level = YandexGame.savesData.DiamondLevel[_id];
        return level;
    }

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Diamonds >= _currentPrice;
        return _isPurchaseAvailable;
    }

    protected override void ExecutePurchase()
    {
        Locator.Instance.Wallet.Diamonds -= _currentPrice;
    }

    protected override void SetLevel()
    {
        YandexGame.savesData.DiamondLevel[_id] = Level;
    }

    protected override void PlayParticle()
    {
        Locator.Instance.Particle.DiamondTransform.position = _upgradeButton.transform.position;
        Locator.Instance.Particle.DiamondParticle.Play();
    }

    protected override double CalculateUpgradePrice()
    {
        return _baseUpgradePrice + Level * _fixedIncreasePrice;
    }
}