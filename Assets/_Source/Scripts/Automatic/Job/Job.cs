using System;
using YG;

public class Job : AutoBase
{
    private readonly double _increaseEveryLevel = 20;

    public override void GetCurrentIncome()
    {
        CurrentIncome = Math.Round(_baseIncome * Level * Math.Pow(_increasePercent, Math.Floor(Level / _increaseEveryLevel)) * _personalUpgrade *
            Modifier.JobIncomeModifier * Modifier.TimeMoneyBoost * Modifier.DiamondIncome * Modifier.ADsBoost);
    }

    protected override void SaveLevel()
    {
        YandexGame.savesData.JobLevel[_id] = Level;
    }

    protected override double NextIncome(int level)
    {
        return Math.Round(_baseIncome * level * Math.Pow(_increasePercent, Math.Floor(level / _increaseEveryLevel)) * _personalUpgrade *
            Modifier.JobIncomeModifier * Modifier.TimeMoneyBoost * Modifier.DiamondIncome * Modifier.ADsBoost);
    }

    protected override void CalculateIncome()
    {
        Locator.Instance.JobsManager.CalculateIncome();
    }
}