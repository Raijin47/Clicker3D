using YG;
using System;

public class Pet : AutoBase
{
    private readonly double _increaseEveryLevel = 10;

    public override void GetCurrentIncome()
    {
        CurrentIncome = Math.Round(_baseIncome * Level * Math.Pow(_increasePercent, Math.Floor(Level / _increaseEveryLevel)) * _upgradesBase.Modifier *
            Modifier.PetIncomeModifier * Modifier.DiamondIncome * Modifier.TimeLoveBoost * Modifier.ADsBoost);       
    }

    protected override void SaveLevel()
    {
        YandexGame.savesData.PetLevel[_id] = Level;
    }

    protected override double NextIncome(int level)
    {
        return Math.Round(_baseIncome * level * Math.Pow(_increasePercent, Math.Floor(level / _increaseEveryLevel)) * _upgradesBase.Modifier *
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
        switch (Level)
        {
            case >= 1000: _upgradesBase.Show(12); break;
            case >= 750: _upgradesBase.Show(11); break;
            case >= 500: _upgradesBase.Show(10); break;
            case >= 400: _upgradesBase.Show(9); break;
            case >= 350: _upgradesBase.Show(8); break;
            case >= 300: _upgradesBase.Show(7); break;
            case >= 250: _upgradesBase.Show(6); break;
            case >= 200: _upgradesBase.Show(5); break;
            case >= 150: _upgradesBase.Show(4); break;
            case >= 100: _upgradesBase.Show(3); break;
            case >= 50: _upgradesBase.Show(2); break;
            case >= 10: _upgradesBase.Show(1); break;
        }
    }

    protected override void UpdateScale()
    {
        double a = Level / _increaseEveryLevel;

        float b = (float)(a - Math.Floor(a));

        _fillImage.fillAmount = b;
    }
}