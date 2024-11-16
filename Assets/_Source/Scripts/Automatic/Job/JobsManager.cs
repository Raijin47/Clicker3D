using YG;

public class JobsManager : AutoBaseManager
{
    protected override void Activate(int i)
    {
        _autoBases[i].Activate(YandexGame.savesData.JobLevel[i]);
    }

    protected override void AddAdditionalListener()
    {
        GlobalEvent.OnIncreaseJobIncome.AddListener(RecalculateAutoBase);
    }

    public override void CalculateIncome()
    {
        base.CalculateIncome();
        GlobalEvent.SendChangeJobIncome();
        GlobalEvent.SendIncreaseClick();
    }

    protected override void GetIncome()
    {
        Locator.Instance.Wallet.Money += _income;
    }

    protected override void SaveAuto()
    {
        YandexGame.savesData.CurrentJob = _id;
    }

    protected override string Currency()
    {
        return TextUtility.GoldImg;
    }
}