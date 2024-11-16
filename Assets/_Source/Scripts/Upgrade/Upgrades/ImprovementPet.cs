using Assets.SimpleLocalization;
using YG;

public class ImprovementPet : ImprovementBase
{
    public double ModifierPercent
    {
        get => _data.IncreasesPercent[ActiveGrade];
    }

    public override int ActiveGrade 
    {
        get => YandexGame.savesData.UpgradePet[_id];
        set => YandexGame.savesData.UpgradePet[_id] = value;
    }

    protected override void Localize()
    {
        string Grade = LocalizationManager.Localize(TextUtility.Grade + _data.Sex +_nextGrade);
        _titleName.text = LocalizationManager.Localize(_data.Name, Grade);

        string Name = TextUtility.GetColorText(LocalizationManager.Localize(TextUtility.ImprovementPetDesName + _id));

        if (_nextGrade == 2 || _nextGrade == 6 || _nextGrade == 10)
        {
            string[] Args = new[]{
            Name,
            TextUtility.GetColorText(LocalizationManager.Localize(TextUtility.ImprovementJobDesName + _id)) };
            _descriptionText.text = LocalizationManager.Localize(TextUtility.ImprovementPetDes1, Args);
        }
        else
        {
            string[] Args = new[]{
                Name,
            TextUtility.GetColorText(_data.IncreasesValue[_nextGrade].ToString()) };
            _descriptionText.text = LocalizationManager.Localize(TextUtility.ImprovementPetDes0, Args);
        }
    }

    protected override void SetTargetUpgrade()
    {
        Locator.Instance.Pets.AutoBases[_id].GetUpgrade();
        Locator.Instance.Jobs.AutoBases[_id].GetUpgrade();
    }
}