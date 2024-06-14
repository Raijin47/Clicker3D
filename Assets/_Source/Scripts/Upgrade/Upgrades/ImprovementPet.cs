using UnityEngine;
using YG;

public class ImprovementPet : ImprovementBase
{
    [SerializeField] private double[] _increasesPercent;

    public double ModifierPercent
    {
        get => _data.IncreasesPercent[ActiveGrade];
    }

    public override int ActiveGrade 
    {
        get => YandexGame.savesData.UpgradePet[_id];
        set => YandexGame.savesData.UpgradePet[_id] = value;
    }

    protected override void SetName()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetTargetUpgrade()
    {
        Locator.Instance.Pets.AutoBases[_id].GetUpgrade();
        Locator.Instance.Jobs.AutoBases[_id].GetUpgrade();
    }
}