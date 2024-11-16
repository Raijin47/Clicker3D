using UnityEngine;
using YG;

public abstract class RebithBase : UpgradeBase
{
    [SerializeField] private double _increaseDegreePrice = 1.3d;

    protected override void AddListener()
    {
        GlobalEvent.OnRebithChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnRebithChange.AddListener(UpdatePriceText);
    }

    public override int Level
    {
        get => YandexGame.savesData.RebithLevel[_id];
        set => YandexGame.savesData.RebithLevel[_id] = value;
    }

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Rebith >= _currentPrice && Level != _maxLevel;
        return _isPurchaseAvailable;
    }

    protected override void ExecutePurchase()
    {
        Locator.Instance.Wallet.Rebith -= _currentPrice;
        SFXController.OnUpgradeRebith?.Invoke();
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

    protected override string Currency()
    {
        return TextUtility.PrestigeImg;
    }
}