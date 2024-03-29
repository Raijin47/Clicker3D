using System;
using YG;

public class Job : AutoBase
{
    public override void UpdateValue()
    {
        CurrentIncome = Math.Round(_baseIncome * Level * Modifier.JobIncomeModifier * Modifier.TimeMoneyBoost * Modifier.DiamondIncome * Modifier.ADsBoost);
    }

    protected override void UpdatePrice()
    {
        CurrentPrice = Math.Round(IncreaseValue.Calculate(Level, _basePrice, Locator.Instance.JobsManager.DegreeIncreasePrice) * Modifier.CostReductionModifier);
    }

    protected override void SaveLevel()
    {
        YandexGame.savesData.JobLevel[_id] = Level;
    }

    protected override double NextIncome(int level)
    {
        return Math.Round(_baseIncome * level * Modifier.JobIncomeModifier * Modifier.TimeMoneyBoost * Modifier.DiamondIncome * Modifier.ADsBoost);
    }

    protected override void CalculateIncome()
    {
        Locator.Instance.JobsManager.CalculateIncome();
    }
}