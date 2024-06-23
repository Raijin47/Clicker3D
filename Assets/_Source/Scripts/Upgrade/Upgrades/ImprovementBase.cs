using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ImprovementBase : MonoBehaviour
{
    [SerializeField] protected int _id;

    [SerializeField] private Image _frameImage;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _upgradeObject;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] protected TextMeshProUGUI _descriptionText;
    [SerializeField] protected TextMeshProUGUI _titleName;
    [SerializeField] protected ImprovementData _data;

    private double _modifier;
    private int _availableGrade;
    protected int _currentGrade;

    public double Modifier
    {
        get => _modifier;
        set
        {
            _modifier = value; 
            SetTargetUpgrade();
        }
    }

    public abstract int ActiveGrade
    {
        get;
        set;
    }

    public void Init()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        _currentGrade = ActiveGrade + 1;
        Modifier = CalculateModifier();
        UpdateUI();
    }

    private void OnEnable()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnDisable()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    public void PurchasedUpgrade()
    {
        if(IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _data.Price[_currentGrade];
            Activate();
        }
    }

    protected abstract void Localize();

    public void Show(int value)
    {
        if (value <= _availableGrade) return;

        _availableGrade = value;
        UpdateUI();      
    }

    private void UpdateUI()
    {
        if (_currentGrade == 0) return;
        _priceText.text = ConvertNumber.Convert(_data.Price[_currentGrade]);
        _frameImage.color = Locator.Instance.Improvement.Color[_currentGrade];
        _upgradeObject.SetActive(ActiveGrade < _availableGrade);
        Localize();
    }


    public void Activate()
    {
        ActiveGrade = _currentGrade;
        _currentGrade = ActiveGrade + 1;

        Modifier = CalculateModifier();
        UpdateUI();
    }

    private double CalculateModifier()
    {
        double value = 1;
        for(int i = 0; i <= ActiveGrade; i++)
        {
            value *= _data.IncreasesValue[i];
        }
        return value;
    }

    protected abstract void SetTargetUpgrade();

    public void Deactivate()
    {
        Modifier = 1;
        _upgradeObject.SetActive(false);
        ActiveGrade = 0;
        _availableGrade = 0;
        _currentGrade = ActiveGrade + 1;
    }

    private void CheckInteractableButton()
    {
        _button.interactable = IsPurchaseAvailable();
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _data.Price[_currentGrade];
        return _isPurchaseAvailable;
    }
}