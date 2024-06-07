using System;
using YG;

public class Job : AutoBase
{
    private readonly double _increaseEveryLevel = 20;

    public override void GetCurrentIncome()
    {
        CurrentIncome = Math.Round(_baseIncome * Level * Math.Pow(_increasePercent, Math.Floor(Level / _increaseEveryLevel)) * _upgradesBase.Modifier *
            Modifier.JobIncomeModifier * Modifier.TimeMoneyBoost * Modifier.DiamondIncome * Modifier.ADsBoost);
    }

    protected override void SaveLevel()
    {
        YandexGame.savesData.JobLevel[_id] = Level;
    }

    protected override double NextIncome(int level)
    {
        return Math.Round(_baseIncome * level * Math.Pow(_increasePercent, Math.Floor(level / _increaseEveryLevel)) * _upgradesBase.Modifier *
            Modifier.JobIncomeModifier * Modifier.TimeMoneyBoost * Modifier.DiamondIncome * Modifier.ADsBoost);
    }

    protected override void CalculateIncome()
    {
        Locator.Instance.JobsManager.CalculateIncome();
    }

    protected override void UnlockUpgrade()
    {
        //switch(Level)
        //{
        //    case >= 1000: _upgradesBase.Show(12); break;
        //    case >= 750: _upgradesBase.Show(11); break;
        //    case >= 500: _upgradesBase.Show(10); break;
        //    case >= 350: _upgradesBase.Show(9); break;
        //    case >= 300: _upgradesBase.Show(8); break;
        //    case >= 250: _upgradesBase.Show(7); break;
        //    case >= 200: _upgradesBase.Show(6); break;
        //    case >= 150: _upgradesBase.Show(5); break;
        //    case >= 100: _upgradesBase.Show(4); break;
        //    case >= 50: _upgradesBase.Show(3); break;
        //    case >= 25: _upgradesBase.Show(2); break;
        //    case >= 1: _upgradesBase.Show(1); break;
        //}

        switch (Level)
        {
            case >= 12: _upgradesBase.Show(12); break;
            case >= 11: _upgradesBase.Show(11); break;
            case >= 10: _upgradesBase.Show(10); break;
            case >= 9: _upgradesBase.Show(9); break;
            case >= 8: _upgradesBase.Show(8); break;
            case >= 7: _upgradesBase.Show(7); break;
            case >= 6: _upgradesBase.Show(6); break;
            case >= 5: _upgradesBase.Show(5); break;
            case >= 4: _upgradesBase.Show(4); break;
            case >= 3: _upgradesBase.Show(3); break;
            case >= 2: _upgradesBase.Show(2); break;
            case >= 1: _upgradesBase.Show(1); break;
        }
    }

    protected override void UpdateScale()
    {
        double a = Level / _increaseEveryLevel;

        float b = (float)(a - Math.Floor(a));

        _fillImage.fillAmount = b;
    }
}