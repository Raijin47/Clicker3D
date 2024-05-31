using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class AutoBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] protected int _id;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Button _buttonUpgrade;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _incomeText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] protected double _baseIncome;
    [SerializeField] protected double _basePrice;
    [SerializeField] protected UpgradesBase[] _upgradesBases;
    protected double _personalUpgrade = 1;

    private Coroutine _upgradeProcessCoroutine;

    private readonly double _degreeIncreasePrice = 1.15;
    protected readonly double _increasePercent = 1.5;
    private readonly WaitForSeconds _intervalToUpgrade = new(.5f);

    protected double _currentIncome;
    protected double _currentPrice;
    private int _level;

    public double CurrentIncome
    {
        get => _currentIncome;
        protected set
        {
            _currentIncome = Math.Round(value);
            _incomeText.text = ConvertNumber.Convert(CurrentIncome) + " > " + ConvertNumber.Convert(NextIncome(_level + 1));
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
            SaveLevel();
        }
    }

    private void UnlockUpgrade()
    {
        if(Level >= 50 && !_upgradesBases[0].IsShow)
        {
            _upgradesBases[0].Show();
        }
        if(Level >= 75 && !_upgradesBases[1].IsShow)
        {
            _upgradesBases[1].Show();
        }
    }

    public void GetUpgrade(double value)
    {
        _personalUpgrade *= value;

        GetCurrentIncome();
        CalculateIncome();
    }

    public virtual void Activate(int level)
    {
        _gameObject.SetActive(true);

        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnCostReduction.AddListener(UpdatePrice);

        Level = level;
    }

    protected void UpdatePrice()
    {
        CurrentPrice = Math.Round(IncreaseValue.Calculate(Level, _basePrice, _degreeIncreasePrice) * Modifier.CostReductionModifier);
    }

    protected abstract void SaveLevel();
    public abstract void GetCurrentIncome();
    protected abstract void CalculateIncome();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPurchaseAvailable())
        {
            Upgrade();
            Locator.Instance.Particle.GoldTransform.position = Input.mousePosition;
            Locator.Instance.Particle.GoldParticle.Play();
            _upgradeProcessCoroutine = StartCoroutine(UpgradeProcess());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_upgradeProcessCoroutine != null)
        {
            StopCoroutine(_upgradeProcessCoroutine);
            _upgradeProcessCoroutine = null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_upgradeProcessCoroutine != null)
        {
            StopCoroutine(_upgradeProcessCoroutine);
            _upgradeProcessCoroutine = null;
        }
    }

    private IEnumerator UpgradeProcess()
    {
        yield return _intervalToUpgrade;
        while (IsPurchaseAvailable())
        {
            Upgrade();
            yield return null;
        }
    }

    private void Upgrade()
    {
        Locator.Instance.Wallet.Money -= _currentPrice;
        Level++;
        GetCurrentIncome();
        CalculateIncome();
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
        GlobalEvent.OnMoneyChange.RemoveListener(CheckInteractableButton);
        GlobalEvent.OnCostReduction.RemoveListener(GetCurrentIncome);
        _personalUpgrade = 1;
        Level = 0;
        _gameObject.SetActive(false);
    }
}