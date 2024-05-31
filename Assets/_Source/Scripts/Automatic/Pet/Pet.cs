using YG;
using System;

public class Pet : AutoBase
{
    private readonly double _increaseEveryLevel = 10;

    public override void GetCurrentIncome()
    {
        CurrentIncome = Math.Round(_baseIncome * Level * Math.Pow(_increasePercent, Math.Floor(Level / _increaseEveryLevel)) * _personalUpgrade *
            Modifier.PetIncomeModifier * Modifier.DiamondIncome * Modifier.TimeLoveBoost * Modifier.ADsBoost);       
    }

    protected override void SaveLevel()
    {
        YandexGame.savesData.PetLevel[_id] = Level;
    }

    protected override double NextIncome(int level)
    {
        return Math.Round(_baseIncome * level * Math.Pow(_increasePercent, Math.Floor(level / _increaseEveryLevel)) * _personalUpgrade *
            Modifier.PetIncomeModifier * Modifier.DiamondIncome * Modifier.TimeLoveBoost * Modifier.ADsBoost);
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