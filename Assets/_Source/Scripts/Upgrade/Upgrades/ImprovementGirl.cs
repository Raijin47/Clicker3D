public class ImprovementGirl : ImprovementPet
{
    protected override void SetTargetUpgrade()
    {
        Locator.Instance.Pets.AutoBases[_id].GetUpgrade();
        GlobalEvent.SendIncreaseClick();
    }
}