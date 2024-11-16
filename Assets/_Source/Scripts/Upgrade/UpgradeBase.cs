using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.SimpleLocalization;

public abstract class UpgradeBase : MonoBehaviour
{
    [SerializeField] protected int _id;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] protected TextMeshProUGUI _priceText;
    [SerializeField] protected TextMeshProUGUI _effectText;
    [SerializeField] protected Button _upgradeButton;

    [SerializeField] protected double _baseUpgradePrice;
    [SerializeField] protected double _baseValue;
    [SerializeField] protected double _fixedIncreaseValue;
    [SerializeField] protected int _maxLevel;

    protected string _currentPriceText;

    protected double _currentValue;
    protected double _currentPrice;
    protected double _nextValue;

    public abstract int Level
    {
        get;
        set;
    }

    public void Init()
    {
        _upgradeButton.onClick.AddListener(UpgradeButton);
        AddListener();
        UpdateValue();
    }

    private void OnEnable()
    {
        LocalizationManager.LocalizationChanged += UpdatePriceText;
    }

    private void OnDisable()
    {
        LocalizationManager.LocalizationChanged -= UpdatePriceText;
    }

    private void UpgradeButton()
    {
        if (IsPurchaseAvailable())
        {
            Level++;
            ExecutePurchase();
            UpdateValue();
            PlayParticle();
        }
    }

    protected abstract void PlayParticle();

    protected void CheckInteractableButton()
    {
        _upgradeButton.interactable = IsPurchaseAvailable();
    }

    protected void UpdateValue()
    {
        _currentValue = CalculateUpgradeValue();
        _nextValue = CalculateUpgradeNext();
        _currentPrice = CalculateUpgradePrice();
        _currentPriceText = ConvertNumber.Convert(_currentPrice);

        Execute();
        UpdateUI();
        CheckInteractableButton();
    }

    protected virtual double CalculateUpgradeValue()
    {
        return _baseValue + Level * _fixedIncreaseValue;
    }
    protected virtual double CalculateUpgradeNext()
    {
        return _baseValue + (Level + 1) * _fixedIncreaseValue;
    }

    protected void UpdateUI()
    {
        if (Level != _maxLevel) UpdateTextProcess();
        else UpdateTextMax();

        _levelText.text = Level.ToString();
        UpdatePriceText();
    }

    protected void UpdatePriceText()
    {
        if (Level != _maxLevel)
        {
            _priceText.text = IsPurchaseAvailable() ?
                TextUtility.GetBlackText(GetPriceText()) :
                TextUtility.GetWhiteText(GetPriceText());
        }
        else
        {
            _priceText.text = TextUtility.GetWhiteText(
                LocalizationManager.Localize(TextUtility.Max));       
        }
    }

    private string GetPriceText()
    {
        string text = LocalizationManager.Localize(TextUtility.Improve) + "\n" + Currency() + _currentPriceText;
        return text;
    }

    protected abstract string Currency();
    protected abstract double CalculateUpgradePrice();
    protected abstract void Execute();
    protected abstract void AddListener();
    protected abstract void ExecutePurchase();
    protected abstract bool IsPurchaseAvailable();
    protected abstract void UpdateTextMax();
    protected abstract void UpdateTextProcess();
}
