using YG;

public abstract class EnhancementBase : UpgradeBase
{
    private const double _degreeIncreasePrice = 1.15;

    protected override void AddListener()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnRebith.AddListener(ResetLevel);
        GlobalEvent.OnCostReduction.AddListener(UpdatePrice);
    }

    protected override int GetLevel()
    {
        int level = YandexGame.savesData.EnchancementLevel[_id];
        return level;
    }

    private void UpdatePrice()
    {
        _currentPrice = CalculateUpgradePrice();
        CheckInteractableButton();
    }

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _currentPrice;
        return _isPurchaseAvailable;
    }

    protected override void ExecutePurchase()
    {
        Locator.Instance.Wallet.Money -= _currentPrice;
    }

    protected virtual void ResetLevel()
    {
        Level = 0;
        UpdateValue();
    }

    protected override void SetLevel()
    {
        YandexGame.savesData.EnchancementLevel[_id] = Level;
    }

    protected override double CalculateUpgradePrice()
    {
        return IncreaseValue.Calculate(Level, _baseUpgradePrice, _degreeIncreasePrice) * Modifier.CostReductionModifier;
    }

    protected override void PlayParticle()
    {
        Locator.Instance.Particle.GoldTransform.position = _upgradeButton.transform.position;
        Locator.Instance.Particle.GoldParticle.Play();
    }
}