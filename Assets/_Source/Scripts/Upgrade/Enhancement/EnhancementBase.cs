using YG;
using System;

public abstract class EnhancementBase : UpgradeBase
{
    private const double _degreeIncreasePrice = 1.15;
    private double _price1;
    private double _price10;
    private double _price100;
    
    protected override void AddListener()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnRebith.AddListener(ResetLevel);
        GlobalEvent.OnCostReduction.AddListener(UpdatePrice);
        GlobalEvent.OnChangeCountUpgrade.AddListener(SwitchPrice);
    }

    protected override int GetLevel()
    {
        int level = YandexGame.savesData.EnchancementLevel[_id];
        return level;
    }

    public void UpdatePrice()
    {
        _currentPrice = CalculateUpgradePrice();
        _priceText.text = ConvertNumber.Convert(_currentPrice);
        CheckInteractableButton();
    }

    private void SwitchPrice()
    {
        switch (Locator.Instance.CountMoneyUpgrade.CurrentState)
        {
            case CountUpgradeButton.CountState.x1: _currentPrice = _price1; break;
            case CountUpgradeButton.CountState.x10: _currentPrice = _price10; break;
            case CountUpgradeButton.CountState.x100: _currentPrice = _price100; break;
        }

        _priceText.text = ConvertNumber.Convert(_currentPrice);
    }

    protected override void UpgradeLevel()
    {
        switch (Locator.Instance.CountMoneyUpgrade.CurrentState)
        {
            case CountUpgradeButton.CountState.x1: Level += 1; break;
            case CountUpgradeButton.CountState.x10: Level += 10; break;
            case CountUpgradeButton.CountState.x100: Level += 100; break;
        }
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
        double currentLevel = Level;
        double value = 0;

        value += IncreaseValue.Calculate(currentLevel, _baseUpgradePrice, _degreeIncreasePrice) * Modifier.CostReductionModifier;
        currentLevel++;

        _price1 = Math.Round(value);

        for (int i = 0; i < 9; i++)
        {
            currentLevel++;
            value += IncreaseValue.Calculate(currentLevel, _baseUpgradePrice, _degreeIncreasePrice) * Modifier.CostReductionModifier;
        }

        _price10 = Math.Round(value);

        for (int i = 0; i < 89; i++)
        {
            currentLevel++;
            value += IncreaseValue.Calculate(currentLevel, _baseUpgradePrice, _degreeIncreasePrice) * Modifier.CostReductionModifier;
        }

        _price100 = Math.Round(value);

        SwitchPrice();

        return _currentPrice;
    }

    protected override void PlayParticle()
    {
        Locator.Instance.Particle.GoldTransform.position = _upgradeButton.transform.position;
        Locator.Instance.Particle.GoldParticle.Play();
    }
}