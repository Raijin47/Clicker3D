using UnityEngine;
using TMPro;
using YG;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _diamondsText;
    [SerializeField] private TextMeshProUGUI _rebithText;

    private double _money;
    private double _diamonds;
    private double _rebith;

    public void Init()
    {
        GlobalEvent.OnRebith.AddListener(OnReset);

        Money = YandexGame.savesData.Money;
        Diamonds = YandexGame.savesData.Diamonds;
        Rebith = YandexGame.savesData.Rebith;
    }

    public double Money
    {
        get => _money;
        set
        {
            _money = value;
            _moneyText.text = ConvertNumber.Convert(_money);
            GlobalEvent.SendChangeMoney();
            YandexGame.savesData.Money = _money;
        }
    }

    public double Diamonds
    {
        get => _diamonds;
        set
        {
            _diamonds = value;
            _diamondsText.text = ConvertNumber.Convert(_diamonds);
            GlobalEvent.SendChangeDiamonds();
            YandexGame.savesData.Diamonds = _diamonds;
        }
    }

    public double Rebith
    {
        get => _rebith;
        set
        {
            _rebith = value;
            _rebithText.text = ConvertNumber.Convert(_rebith);
            GlobalEvent.SendChangeRebith();
            YandexGame.savesData.Rebith = _rebith;
        }
    }

    private void OnReset()
    {
        Money = 0;
    }
}