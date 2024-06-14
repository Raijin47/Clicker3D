using YG;

public class ImprovementJob : ImprovementBase
{
    public override int ActiveGrade
    {
        get => YandexGame.savesData.UpgradeJob[_id];
        set => YandexGame.savesData.UpgradeJob[_id] = value;
    }

    protected override void SetName()
    {

    }

    protected override void SetTargetUpgrade()
    {
        Locator.Instance.Jobs.AutoBases[_id].GetUpgrade();
    }
}