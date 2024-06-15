using YG;

public class ImprovementJob : ImprovementBase
{
    public override int ActiveGrade
    {
        get => YandexGame.savesData.UpgradeJob[_id];
        set => YandexGame.savesData.UpgradeJob[_id] = value;
    }

    protected override void Localize()
    {
        //throw new System.NotImplementedException();
    }

    //protected override void SetName()
    //{
    //    //_titleName.SetKeyName(TextUtility.ImprovementPetName + _id);
    //    _titleName.SetAdditionalKey(TextUtility.JobGrade + _currentGrade);
    //}

    protected override void SetTargetUpgrade()
    {
        Locator.Instance.Jobs.AutoBases[_id].GetUpgrade();
    }
}