using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public abstract class AutoBase : MonoBehaviour
{
    [SerializeField] protected int _id;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Button _buttonUpgrade;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _incomeText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] protected double _baseIncome;
    [SerializeField] protected double _basePrice;
    [SerializeField] protected Image _fillImage;

    protected double _price1;
    protected double _price10;
    protected double _price100;

    private const double _degreeIncreasePrice = 1.15;
    protected const double _increasePercent = 1.5;

    protected double _currentIncome;
    protected double _currentPrice;
    private int _level;

    public double CurrentIncome
    {
        get => _currentIncome;
        protected set
        {
            _currentIncome = Math.Round(value);

            _incomeText.text = ConvertNumber.Convert(_currentIncome) + TextUtility.MoreSign + TextUtility.GetColorText(ConvertNumber.Convert(NextIncome(_level + 1)));
        }
    }

    protected abstract double NextIncome(int level);

    protected double CurrentPrice
    {
        get => _currentPrice;
        set
        {
            _currentPrice = Math.Round(value);
            _priceText.text = ConvertNumber.Convert(_currentPrice);
            CheckInteractableButton();
        }
    }

    public int Level
    {
        get => _level;
        protected set
        {
            _level = value;
            _levelText.text = _level.ToString();
            UnlockUpgrade();
            GetCurrentIncome();
            UpdatePrice();
            UpdateScale();
            SaveLevel();
        }
    }

    protected abstract void UpdateScale();

    protected abstract void UnlockUpgrade();

    public void GetUpgrade()
    {
        GetCurrentIncome();
        CalculateIncome();
    }

    public virtual void Activate(int level)
    {
        _gameObject.SetActive(true);

        Level = level;
    }

    protected virtual void OnEnable()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnChangeCountUpgrade.AddListener(SwitchPrice);
        _buttonUpgrade.onClick.AddListener(UpgradeButton);
    }

    protected virtual void OnDisable()
    {
        GlobalEvent.OnMoneyChange.RemoveListener(CheckInteractableButton);
        GlobalEvent.OnChangeCountUpgrade.RemoveListener(SwitchPrice);
        _buttonUpgrade.onClick.RemoveListener(UpgradeButton);
    }

    protected void UpdatePrice()
    {
        double currentLevel = Level;
        double value = 0;

        value += Math.Round(IncreaseValue.Calculate(currentLevel, _basePrice, _degreeIncreasePrice) * CostReduction());
        currentLevel++;

        _price1 = Math.Round(value);

        for (int i = 0; i < 9; i++)
        {
            currentLevel++;
            value += Math.Round(IncreaseValue.Calculate(currentLevel, _basePrice, _degreeIncreasePrice) * CostReduction());
        }

        _price10 = Math.Round(value);

        for (int i = 0; i < 89; i++)
        {
            currentLevel++;
            value += Math.Round(IncreaseValue.Calculate(currentLevel, _basePrice, _degreeIncreasePrice) * CostReduction());
        }

        _price100 = Math.Round(value);

        SwitchPrice();
    }

    protected abstract void SwitchPrice();
    protected abstract void SaveLevel();
    protected abstract double CostReduction();
    protected abstract void UpdateLevel();
    public abstract void GetCurrentIncome();
    protected abstract void CalculateIncome();

    
    public void UpgradeButton()
    {
        if (IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _currentPrice;
            UpdateLevel();
            GetCurrentIncome();
            CalculateIncome();
            Locator.Instance.Particle.GoldTransform.position = _buttonUpgrade.transform.position;
            Locator.Instance.Particle.GoldParticle.Play();
        }
    }

    private void CheckInteractableButton()
    {
        _buttonUpgrade.interactable = IsPurchaseAvailable();
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _currentPrice;
        return _isPurchaseAvailable;
    }

    public virtual void Deactivate()
    {
        Level = 0;
        _gameObject.SetActive(false);
    }
}