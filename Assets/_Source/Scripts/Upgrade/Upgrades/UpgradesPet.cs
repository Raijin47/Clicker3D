using UnityEngine;

public class UpgradesPet : UpgradesBase
{
    [SerializeField] private double[] _increasesPercent;

    public double ModifierPercent
    {
        get => _increasesPercent[ActiveGrade];
    }
}