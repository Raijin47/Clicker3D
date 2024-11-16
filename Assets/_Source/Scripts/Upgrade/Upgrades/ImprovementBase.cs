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
    protected int _nextGrade;

    private string _currentPriceText;

    public double Modifier
    {
        get => _modifier;
        set
        {
            _modifier = value; 
            SetTargetUpgrade();
        }
    }

    public double Price => _data.Price[ActiveGrade];

    public abstract int ActiveGrade
    {
        get;
        set;
    }

    public void Init()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        _nextGrade = ActiveGrade + 1;
        _modifier = CalculateModifier();
        UpdateUI();
    }

    private void OnEnable()
    {
        LocalizationManager.LocalizationChanged += Localize;
        LocalizationManager.LocalizationChanged += UpdatePriceText;
    }

    private void OnDisable()
    {
        LocalizationManager.LocalizationChanged += Localize;
        LocalizationManager.LocalizationChanged -= UpdatePriceText;
    }

    public void PurchasedUpgrade()
    {
        if (IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _data.Price[ActiveGrade];

            Locator.Instance.Particle.GoldTransform.position = _button.transform.position;
            Locator.Instance.Particle.GoldParticle.Play();

            SFXController.OnUpgradeMoney?.Invoke();

            Activate();
        }
    }

    protected abstract void Localize();

    public void Show(int value)
    {
        if(value > _availableGrade)
        {
            _availableGrade = value;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _currentPriceText = ConvertNumber.Convert(_data.Price[ActiveGrade]);
        UpdatePriceText();
        _frameImage.color = Locator.Instance.Improvement.Color[ActiveGrade];
        _upgradeObject.SetActive(ActiveGrade < _availableGrade);
        Localize();
        Locator.Instance.Improvement.SortingImproved();
    }

    private void UpdatePriceText()
    {
        _priceText.text = IsPurchaseAvailable() ?
            TextUtility.GetBlackText( GetPriceText()) : 
            TextUtility.GetWhiteText(GetPriceText());
    }

    private string GetPriceText()
    {
        string text = LocalizationManager.Localize(TextUtility.Explore) + "\n" + TextUtility.GoldImg + _currentPriceText;
        return text;
    }

    public void Activate()
    {
        ActiveGrade = _nextGrade;
        _nextGrade++;

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
        _nextGrade = 1;
    }

    private void CheckInteractableButton()
    {
        _button.interactable = IsPurchaseAvailable();
        UpdatePriceText();
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _data.Price[ActiveGrade];
        return _isPurchaseAvailable;
    }
}