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
    [SerializeField] private LocalizedDynamic _effectText;
    [SerializeField] private LocalizedText _gradeText;
    [SerializeField] protected ImprovementData _data;
    [SerializeField] private string[] _gradeKeys;

    private int _availableGrade;
    private int _currentGrade;

    public double Modifier
    {
        get => _data.IncreasesValue[ActiveGrade];
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
        UpdateUI();
    }

    public void PurchasedUpgrade()
    {
        if(IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _data.Price[_currentGrade];
            Activate();
        }
    }

    public void Show(int value)
    {
        if (value <= _availableGrade) return;

        _availableGrade = value;
        UpdateUI();      
    }

    //switching keys in scriptable object
    private void UpdateUI()
    {
        if (_currentGrade == 0) return;
        _priceText.text = ConvertNumber.Convert(_data.Price[_currentGrade]);
        _effectText.SetValue((_data.IncreasesValue[_currentGrade] / _data.IncreasesValue[_currentGrade-1]).ToString());
        _frameImage.color = Locator.Instance.Improvement.Color[_currentGrade];
        //_gradeText.SetKey(_gradeKeys[_currentGrade]);
        _upgradeObject.SetActive(ActiveGrade < _availableGrade);
        SetName();
    }

    protected abstract void SetName();

    public void Activate()
    {
        ActiveGrade = _currentGrade;
        _currentGrade = ActiveGrade + 1;
        SetTargetUpgrade();
        UpdateUI();
    }

    protected abstract void SetTargetUpgrade();

    public void Deactivate()
    {
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