using UnityEngine;
using TMPro;
using YG;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _diamondsText;
    [SerializeField] private TextMeshProUGUI _rebithText;

    public void Init()
    {
        GlobalEvent.OnRebith.AddListener(OnReset);

        _moneyText.text = TextUtility.GoldImg + ConvertNumber.Convert(Money);
        _diamondsText.text = TextUtility.DiamondImg + ConvertNumber.Convert(Diamonds);
        _rebithText.text = TextUtility.PrestigeImg + ConvertNumber.Convert(Rebith);
    }

    public double Money
    {
        get => YandexGame.savesData.Money;
        set
        {
            YandexGame.savesData.Money = value;
            _moneyText.text = TextUtility.GoldImg + ConvertNumber.Convert(Money);
            GlobalEvent.SendChangeMoney();
            YandexGame.SaveLocal();
        }
    }

    public double Diamonds
    {
        get => YandexGame.savesData.Diamonds;
        set
        {
            YandexGame.savesData.Diamonds = value;
            _diamondsText.text = TextUtility.DiamondImg + ConvertNumber.Convert(Diamonds);
            GlobalEvent.SendChangeDiamonds();
            YandexGame.SaveProgress();
        }
    }

    public double Rebith
    {
        get => YandexGame.savesData.Rebith;
        set
        {
            YandexGame.savesData.Rebith = value;
            _rebithText.text = TextUtility.PrestigeImg + ConvertNumber.Convert(Rebith);
            GlobalEvent.SendChangeRebith();
            YandexGame.SaveProgress();
        }
    }

    private void OnReset()
    {
        Money = 0;
    }
}