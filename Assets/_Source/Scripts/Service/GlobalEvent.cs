using UnityEngine.Events;

public class GlobalEvent
{
    public static UnityEvent OnMoneyChange = new();
    public static UnityEvent OnDiamondChange = new();
    public static UnityEvent OnRebithChange = new();
    public static UnityEvent OnRebith = new();
    public static UnityEvent OnStageChange = new();
    public static UnityEvent OnIncreaseJobIncome = new();
    public static UnityEvent OnIncreaseClick = new();
    public static UnityEvent OnCostReduction = new();
    public static UnityEvent OnHealthReduction = new();
    public static UnityEvent OnReductionLostDays = new();
    public static UnityEvent OnIncreasePetIncome = new();
    public static UnityEvent OnIncreaseRebithMultiplier = new();
    public static UnityEvent OnChangeBonusDurationTime = new();
    public static UnityEvent OnChangeModifireBonusAds = new();
    public static UnityEvent OnChangeJobIncome = new();

    public static void SendChangeMoney() => OnMoneyChange.Invoke();
    public static void SendChangeDiamonds() => OnDiamondChange.Invoke();
    public static void SendChangeRebith() => OnRebithChange.Invoke();
    public static void SendRebith() => OnRebith.Invoke();
    public static void SendChangeStage() => OnStageChange.Invoke();
    public static void SendIncreaseJobIncome() => OnIncreaseJobIncome.Invoke();
    public static void SendIncreasePetIncome() => OnIncreasePetIncome.Invoke();
    public static void SendIncreaseClick() => OnIncreaseClick.Invoke();
    public static void SendCostReduction() => OnCostReduction.Invoke();
    public static void SendHealthReduction() => OnHealthReduction.Invoke();
    public static void SendReductionLostDays() => OnReductionLostDays.Invoke();
    public static void SendIncreaseRebithMultiplier() => OnIncreaseRebithMultiplier.Invoke();
    public static void SendChangeBonusDurationTime() => OnChangeBonusDurationTime.Invoke();
    public static void SendChangeModifireBonusAds() => OnChangeModifireBonusAds.Invoke();
    public static void SendChangeJobIncome() => OnChangeJobIncome.Invoke();
}