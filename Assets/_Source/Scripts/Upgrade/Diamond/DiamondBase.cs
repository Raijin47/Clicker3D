using UnityEngine;
using YG;

public abstract class DiamondBase : UpgradeBase
{
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
        Locator.Instance.Particle.DiamondTransform.position = Input.mousePosition;
        Locator.Instance.Particle.DiamondParticle.Play();
    }
}