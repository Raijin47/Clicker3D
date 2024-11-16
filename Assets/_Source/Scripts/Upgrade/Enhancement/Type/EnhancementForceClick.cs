using System;
using YG;
using Assets.SimpleLocalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EnhancementForceClick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _effectText;
    [SerializeField] private Button _upgradeButton;

    private const double UpgradePrice = 17;
    private const double BaseValue = 1;
    private const double DegreeIncreasePrice = 1.15;
    private const int Girl_ID = 9;

    private double _currentValue;
    private double _currentPrice;
    private double _nextValue;

    private string _currentPriceText;
    private double _price1, _price10, _price100;
    private string _price1Text, _price10Text, _price100Text;

    private double _clickForce;
    private string _clickForceText;

    public double ClickForce => _clickForce;
    public string ClickForceText => _clickForceText;

    private int Level
    {
        get => YandexGame.savesData.ClickLevel;
        set
        {
            YandexGame.savesData.ClickLevel = value;
            _levelText.text = Level.ToString();
        }
    }

    public void Init()
    {
        _upgradeButton.onClick.AddListener(UpgradeButton);
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableUI);
        GlobalEvent.OnRebith.AddListener(ResetLevel);
        GlobalEvent.OnCostReductionPurchase.AddListener(CalculateUpgradePrice);
        GlobalEvent.OnChangeCountUpgrade.AddListener(SwitchPrice);
        GlobalEvent.OnIncreaseClick.AddListener(UpdateClickForce);

        _levelText.text = Level.ToString();

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

    private void UpdateValue()
    {
        _currentValue = Level * BaseValue;
        _nextValue = (Level + 1) * BaseValue;
        CalculateUpgradePrice();
        Execute();
    }

    private void CalculateUpgradePrice()
    {
        double currentLevel = Level;
        double value = 0;

        value += IncreaseValue.Calculate(currentLevel, UpgradePrice, DegreeIncreasePrice) * Modifier.CostReductionPurchase;

        _price1 = Math.Round(value);
        _price1Text = ConvertNumber.Convert(_price1);

        for (int i = 0; i < 9; i++)
        {
            currentLevel++;
            value += IncreaseValue.Calculate(currentLevel, UpgradePrice, DegreeIncreasePrice) * Modifier.CostReductionPurchase;
        }

        _price10 = Math.Round(value);
        _price10Text = ConvertNumber.Convert(_price10);

        for (int i = 0; i < 90; i++)
        {
            currentLevel++;
            value += IncreaseValue.Calculate(currentLevel, UpgradePrice, DegreeIncreasePrice) * Modifier.CostReductionPurchase;
        }

        _price100 = Math.Round(value);
        _price100Text = ConvertNumber.Convert(_price100);

        SwitchPrice();
    }

    private void SwitchPrice()
    {
        switch (Locator.Instance.CountMoneyUpgrade.CurrentState)
        {
            case CountUpgradeButton.CountState.x1: 
                _currentPrice = _price1;
                _currentPriceText = _price1Text;
                break;
            case CountUpgradeButton.CountState.x10: 
                _currentPrice = _price10;
                _currentPriceText = _price10Text;
                break;
            case CountUpgradeButton.CountState.x100: 
                _currentPrice = _price100;
                _currentPriceText = _price100Text;
                break;
        }
        UpdatePriceText();
        CheckInteractableButton();
    }

    private void UpdatePriceText()
    {
        _priceText.text = IsPurchaseAvailable() ?
            TextUtility.GetBlackText(GetPriceText()) :
            TextUtility.GetWhiteText(GetPriceText());
    }

    private void CheckInteractableUI()
    {
        UpdatePriceText();
        CheckInteractableButton();
    }

    private string GetPriceText()
    {
        string text = LocalizationManager.Localize(TextUtility.Improve) + "\n" + TextUtility.GoldImg + _currentPriceText;
        return text;
    }

    protected void CheckInteractableButton()
    {
        _upgradeButton.interactable = IsPurchaseAvailable();
    }

    private bool IsPurchaseAvailable()
    {
        bool isPurchaseAvailable = Locator.Instance.Wallet.Money >= _currentPrice;
        return isPurchaseAvailable;
    }

    private void UpgradeButton()
    {
        if (IsPurchaseAvailable())
        {
            AddLevel();
            Locator.Instance.Wallet.Money -= _currentPrice;

            Locator.Instance.Particle.GoldTransform.position = _upgradeButton.transform.position;
            Locator.Instance.Particle.GoldParticle.Play();

            SFXController.OnUpgradeMoney?.Invoke();

            UpdateValue();
        }
    }

    private void AddLevel()
    {
        switch (Locator.Instance.CountMoneyUpgrade.CurrentState)
        {
            case CountUpgradeButton.CountState.x1: Level += 1; break;
            case CountUpgradeButton.CountState.x10: Level += 10; break;
            case CountUpgradeButton.CountState.x100: Level += 100; break;
        }
    }

    private void Execute()
    {
        Modifier.EnhancementClickForce = _currentValue;
        var Improved = Locator.Instance.Improvement.Click;
        switch (Level)
        {
            case >= 1000: Improved.Show(12); break;
            case >= 750: Improved.Show(11); break;
            case >= 500: Improved.Show(10); break;
            case >= 350: Improved.Show(9); break;
            case >= 300: Improved.Show(8); break;
            case >= 250: Improved.Show(7); break;
            case >= 200: Improved.Show(6); break;
            case >= 150: Improved.Show(5); break;
            case >= 100: Improved.Show(4); break;
            case >= 50: Improved.Show(3); break;
            case >= 25: Improved.Show(2); break;
            case >= 1: Improved.Show(1); break;
        }
    }

    private void ResetLevel()
    {
        Level = 1;
        UpdateValue();
    }

    private void UpdateClickForce()
    {
        double multiplyValue = Modifier.ADsBoost * Modifier.TimeMoneyBoost * Locator.Instance.Improvement.Click.Modifier *
            (1 + Locator.Instance.Pets.AutoBases[Girl_ID].Level *
            Locator.Instance.Improvement.Pets[Girl_ID].ModifierPercent) *
            Locator.Instance.Improvement.Island.Value;

        double additionalValue = Locator.Instance.Jobs.CurrentIncome * Locator.Instance.Improvement.Click.ModifierPercent;

        _clickForce = Math.Round(_currentValue * multiplyValue + additionalValue);
        _clickForceText = ConvertNumber.Convert(_clickForce);

        _effectText.text = _clickForceText + TextUtility.MoreSign +
            TextUtility.GetColorText(ConvertNumber.Convert(Math.Round(_nextValue * multiplyValue + additionalValue)));
    }
}