using YG;
using System;
using Assets.SimpleLocalization;

public class Pet : AutoBase
{
    private const double _increaseEveryLevel = 10;

    protected override void SaveLevel()
    {
        YandexGame.savesData.PetLevel[_id] = Level;
        Locator.Instance.Jobs.AutoBases[_id].GetUpgrade();
    }

    public override void GetCurrentIncome()
    {
        CurrentIncome = GetValue(Level);
    }

    protected override double GetNextIncome()
    {
        return GetValue(Level + 1);
    }

    protected override double GetValue(int level)
    {
        return Math.Round(_baseIncome * level * Math.Pow(_increasePercent, Math.Floor(level / _increaseEveryLevel)) *
            Modifier.PetIncomeModifier * Modifier.TimeLoveBoost * Modifier.ADsBoost *
            Locator.Instance.Improvement.Pets[_id].Modifier * Locator.Instance.Improvement.Island.Value);
    }

    public override void Activate(int level)
    {
        base.Activate(level);
        Locator.Instance.Pets.Pets[_id].SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Locator.Instance.Pets.Pets[_id].SetActive(false);
    }

    protected override void CalculateIncome()
    {
        Locator.Instance.Pets.CalculateIncome();
    }

    protected override void UnlockUpgrade()
    {
        switch (Level)
        {
            case >= 1000: Locator.Instance.Improvement.Pets[_id].Show(12); break;
            case >= 750: Locator.Instance.Improvement.Pets[_id].Show(11); break;
            case >= 500: Locator.Instance.Improvement.Pets[_id].Show(10); break;
            case >= 400: Locator.Instance.Improvement.Pets[_id].Show(9); break;
            case >= 350: Locator.Instance.Improvement.Pets[_id].Show(8); break;
            case >= 300: Locator.Instance.Improvement.Pets[_id].Show(7); break;
            case >= 250: Locator.Instance.Improvement.Pets[_id].Show(6); break;
            case >= 200: Locator.Instance.Improvement.Pets[_id].Show(5); break;
            case >= 150: Locator.Instance.Improvement.Pets[_id].Show(4); break;
            case >= 100: Locator.Instance.Improvement.Pets[_id].Show(3); break;
            case >= 50: Locator.Instance.Improvement.Pets[_id].Show(2); break;
            case >= 10: Locator.Instance.Improvement.Pets[_id].Show(1); break;
        }
    }

    protected override void UpdateScale()
    {
        double a = Level / _increaseEveryLevel;

        float b = (float)(a - Math.Floor(a));

        _fillImage.fillAmount = b;
    }

    protected override void UpdateLevel()
    {
        switch (Locator.Instance.CountLoveUpgrade.CurrentState)
        {
            case CountUpgradeButton.CountState.x1: Level += 1; break;
            case CountUpgradeButton.CountState.x10: Level += 10; break;
            case CountUpgradeButton.CountState.x100: Level += 100; break;
        }
    }

    protected override void SwitchPrice()
    {
        switch (Locator.Instance.CountLoveUpgrade.CurrentState)
        {
            case CountUpgradeButton.CountState.x1: 
                CurrentPrice = _price1;
                CurrentPriceText = _price1Text;
                break;
            case CountUpgradeButton.CountState.x10: 
                CurrentPrice = _price10;
                CurrentPriceText = _price10Text;
                break;
            case CountUpgradeButton.CountState.x100: 
                CurrentPrice = _price100;
                CurrentPriceText = _price100Text;
                break;
        }
    }

    protected override double CostReduction()
    {
        return Modifier.CostReductionTraining;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GlobalEvent.OnCostReductionTraining.AddListener(UpdatePrice);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GlobalEvent.OnCostReductionTraining.RemoveListener(UpdatePrice);
    }

    protected override string TypeUpgradeText()
    {
        return LocalizationManager.Localize(TextUtility.Traininig);
    }
}