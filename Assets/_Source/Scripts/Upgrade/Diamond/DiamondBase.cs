using UnityEngine;
using YG;

public abstract class DiamondBase : UpgradeBase
{
    [SerializeField] private double _fixedIncreasePrice;


    protected override void AddListener()
    {
        GlobalEvent.OnDiamondChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnDiamondChange.AddListener(UpdatePriceText);
    }

    public override int Level
    {
        get => YandexGame.savesData.DiamondLevel[_id];
        set => YandexGame.savesData.DiamondLevel[_id] = value;
    }

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Diamonds >= _currentPrice && Level != _maxLevel;
        return _isPurchaseAvailable;
    }

    protected override void ExecutePurchase()
    {
        Locator.Instance.Wallet.Diamonds -= _currentPrice;
        SFXController.OnUpgradeDiamonds?.Invoke();
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

    protected override string Currency()
    {
        return TextUtility.DiamondImg;
    }
}