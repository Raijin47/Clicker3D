using YG;
using System;

public class Pet : AutoBase
{
    public override void UpdateValue()
    {
        CurrentIncome = Math.Round(_baseIncome * Level * Modifier.PetIncomeModifier * Modifier.DiamondIncome * Modifier.TimeLoveBoost * Modifier.ADsBoost);       
    }

    protected override void UpdatePrice()
    {
        CurrentPrice = Math.Round(IncreaseValue.Calculate(Level, _basePrice, Locator.Instance.PetsManager.DegreeIncreasePrice) * Modifier.CostReductionModifier);
    }

    protected override void SaveLevel()
    {
        YandexGame.savesData.PetLevel[_id] = Level;
    }

    protected override double NextIncome(int level)
    {
        return Math.Round(_baseIncome * level * Modifier.PetIncomeModifier * Modifier.DiamondIncome * Modifier.TimeLoveBoost * Modifier.ADsBoost);
    }

    public override void Activate(int level)
    {
        base.Activate(level);
        Locator.Instance.PetsManager.Pets[_id].SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Locator.Instance.PetsManager.Pets[_id].SetActive(false);
    }

    protected override void CalculateIncome()
    {
        Locator.Instance.PetsManager.CalculateIncome();
    }
}