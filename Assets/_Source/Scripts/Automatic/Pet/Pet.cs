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

    protected override void UnlockUpgrade()
    {
        if (Level >= 10 && !_upgradesBases[0].IsShow) _upgradesBases[0].Show();
        if (Level >= 50 && !_upgradesBases[1].IsShow) _upgradesBases[1].Show();
        if (Level >= 100 && !_upgradesBases[2].IsShow) _upgradesBases[2].Show();
        if (Level >= 150 && !_upgradesBases[3].IsShow) _upgradesBases[3].Show();
        if (Level >= 200 && !_upgradesBases[4].IsShow) _upgradesBases[4].Show();
        if (Level >= 250 && !_upgradesBases[5].IsShow) _upgradesBases[5].Show();
        if (Level >= 300 && !_upgradesBases[6].IsShow) _upgradesBases[6].Show();
        if (Level >= 350 && !_upgradesBases[7].IsShow) _upgradesBases[7].Show();
        if (Level >= 400 && !_upgradesBases[8].IsShow) _upgradesBases[8].Show();
        if (Level >= 500 && !_upgradesBases[9].IsShow) _upgradesBases[9].Show();
        if (Level >= 750 && !_upgradesBases[10].IsShow) _upgradesBases[10].Show();
        if (Level >= 1000 && !_upgradesBases[11].IsShow) _upgradesBases[11].Show();
    }
}