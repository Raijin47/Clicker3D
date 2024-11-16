using Assets.SimpleLocalization;
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
        string Grade = LocalizationManager.Localize(TextUtility.Grade + _data.Sex + _nextGrade);
        _titleName.text = LocalizationManager.Localize(_data.Name, Grade);

        string Name = LocalizationManager.Localize(TextUtility.ImprovementJobDesName + _id);

        string[] Args = new[]
        {
            TextUtility.GetColorText(Name),
            TextUtility.GetColorText(_data.IncreasesValue[_nextGrade].ToString())
        };

        _descriptionText.text = LocalizationManager.Localize(TextUtility.ImprovementJobDes, Args);
    }

    protected override void SetTargetUpgrade()
    {
        Locator.Instance.Jobs.AutoBases[_id].GetUpgrade();
    }
}