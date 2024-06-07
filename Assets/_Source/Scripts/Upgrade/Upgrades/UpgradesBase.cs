using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UpgradesBase : MonoBehaviour
{
    [SerializeField] private int _id;

    [SerializeField] private Image _frameImage;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _upgradeObject;
    [SerializeField] private AutoBase _targetUpgrade;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private LocalizedDynamic _effectText;
    [SerializeField] private LocalizedText _gradeText;


    [SerializeField] private Color[] _colors;
    [SerializeField] private string[] _gradeKeys;
    [SerializeField] private double[] _prices;
    [SerializeField] private double[] _increasesValue;

    private int _availableGrade;
    private int _currentGrade;

    public double Modifier
    {
        get => _increasesValue[ActiveGrade];
    }

    public int ActiveGrade
    {
        get => YandexGame.savesData.UpgradeAutomatic[_id];
        set => YandexGame.savesData.UpgradeAutomatic[_id] = value;
    }

    public void Init()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        _currentGrade = ActiveGrade + 1;
        UpdateUI();
    }

    public void PurchasedUpgrade()
    {
        if(IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _prices[_currentGrade];
            Activate();
        }
    }

    public void Show(int value)
    {
        if (value <= _availableGrade) return;

        _availableGrade = value;
        UpdateUI();      
    }


    private void UpdateUI()
    {
        _priceText.text = ConvertNumber.Convert(_prices[_currentGrade]);
        _effectText.SetValue((_increasesValue[_currentGrade] / _increasesValue[_currentGrade-1]).ToString());
        _frameImage.color = _colors[_currentGrade];
        _gradeText.SetKey(_gradeKeys[_currentGrade]);
        _upgradeObject.SetActive(ActiveGrade < _availableGrade);
    }


    public void Activate()
    {
        ActiveGrade = _currentGrade;
        _currentGrade = ActiveGrade + 1;
        _targetUpgrade.GetUpgrade();
        UpdateUI();
    }

    public void Deactivate()
    {
        _upgradeObject.SetActive(false);
    }

    private void CheckInteractableButton()
    {
        _button.interactable = IsPurchaseAvailable();
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _prices[_currentGrade];
        return _isPurchaseAvailable;
    }
}