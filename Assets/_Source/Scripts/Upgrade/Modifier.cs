public static class Modifier
{
    #region Enhancement
    private static double _enhancementClickFocre;
    private static double _enhancementCritChance;
    public static double EnhancementClickForce
    {
        get => _enhancementClickFocre;
        set
        {
            _enhancementClickFocre = value;
            GlobalEvent.SendIncreaseClick();
        }
    }
    public static double EnhancementCritChanceClick
    {
        get => _enhancementCritChance;
        set
        {
            _enhancementCritChance = value * 0.01d;
        }
    }
    #endregion

    #region Diamond
    private static double _diamondIncome;
    private static double _prestigeMultiplier;
    private static double _reductionLostDays;
    private static double _offlineIncomeTime;
    private static double _offlineIncomeModifire;
    private static double _bonusDurationTime;
    private static double _modifireBonusAds;

    public static double DiamondIncome
    {
        get => _diamondIncome;
        set
        {
            _diamondIncome = value * 0.01d + 1d;
            GlobalEvent.SendIncreaseClick();
            GlobalEvent.SendIncreasePetIncome();
            GlobalEvent.SendIncreaseJobIncome();
        }
    }

    public static double PrestigeMultiplier
    {
        get => _prestigeMultiplier;
        set
        {
            _prestigeMultiplier = 1 + value * 0.01d;
            GlobalEvent.SendIncreaseRebithMultiplier();
        }
    }

    public static double ReductionLostDays
    {
        get => _reductionLostDays;
        set
        {
            _reductionLostDays = value * 0.01d;
            GlobalEvent.SendReductionLostDays();
        }
    }

    public static double OfflineIncomeTime
    {
        get => _offlineIncomeTime;
        set
        {
            _offlineIncomeTime = value;
        }
    }

    public static double OfflineIncomeModifire
    {
        get => _offlineIncomeModifire;
        set
        {
            _offlineIncomeModifire = value * 0.01d;
        }
    }

    public static double BonusDurationTime
    {
        get => _bonusDurationTime;
        set 
        {
            _bonusDurationTime = value;
            GlobalEvent.SendChangeBonusDurationTime();
        } 
    }

    public static double ModifireBonusAds
    {
        get => _modifireBonusAds;
        set
        {
            _modifireBonusAds = value;
            GlobalEvent.SendChangeModifireBonusAds();
        }
    }
    #endregion

    #region Rebith
    private static double _prestigeClickForce;
    private static double _chanceSkipStage;
    private static double _healthReductionModifier;
    private static double _costReductionModifier;
    private static double _jobIncomeModifier;
    private static double _petIncomeModifier;
    private static double _startMoney;

    public static double PrestigeClickForce
    {
        get => _prestigeClickForce;
        set
        {
            _prestigeClickForce = value * 0.01d;
            GlobalEvent.SendIncreaseClick();
        }
    }

    public static double ChanceSkipStage {
        get => _chanceSkipStage;
        set
        {
            _chanceSkipStage = value * 0.01d;
        }
    }

    public static double HealthReductionModifier
    {
        get => _healthReductionModifier;
        set
        {
            _healthReductionModifier = (100d - value) * 0.01d;
            GlobalEvent.SendHealthReduction();
        }
    }

    public static double CostReductionModifier
    {
        get => _costReductionModifier;
        set
        {
            _costReductionModifier = (100d - value) * 0.01d;
            GlobalEvent.SendCostReduction();
        }
    }

    public static double JobIncomeModifier
    {
        get => _jobIncomeModifier;
        set
        {
            _jobIncomeModifier = value * 0.01d;
            GlobalEvent.SendIncreaseJobIncome();
        }
    }

    public static double PetIncomeModifier
    {
        get => _petIncomeModifier;
        set
        {
            _petIncomeModifier = value * 0.01d;
            GlobalEvent.SendIncreasePetIncome();
        }
    }

    public static double StartMoney
    {
        get => _startMoney;
        set => _startMoney = value;
    }
    #endregion

    #region ADs

    private static double _adsBoost;
    private static double _timeLoveBoost;
    private static double _timeMoneyBoost;

    public static double ADsBoost
    {
        get => _adsBoost;
        set
        {
            _adsBoost = value * 0.01d + 1;
            GlobalEvent.SendIncreaseClick();
            GlobalEvent.SendIncreasePetIncome();
            GlobalEvent.SendIncreaseJobIncome();
        }
    }

    public static double TimeLoveBoost
    {
        get => _timeLoveBoost;
        set
        {
            _timeLoveBoost = value;
            GlobalEvent.SendIncreasePetIncome();
        }
    }

    public static double TimeMoneyBoost
    {
        get => _timeMoneyBoost;
        set
        {
            _timeMoneyBoost = value;
            GlobalEvent.SendIncreaseClick();
            GlobalEvent.SendIncreaseJobIncome();
        }
    }
    #endregion
}