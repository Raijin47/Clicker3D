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

    private Coroutine _upgradeProcessCoroutine;
    private WaitForSeconds _intervalToUpgrade = new WaitForSeconds(.5f);

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
            UpdateValue();
            UpdatePrice();
            SaveLevel();
        }
    }

    public virtual void Activate(int level)
    {
        _gameObject.SetActive(true);

        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        GlobalEvent.OnCostReduction.AddListener(UpdatePrice);

        Level = level;
    }
    protected abstract void UpdatePrice();
    protected abstract void SaveLevel();
    public abstract void UpdateValue();
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
        UpdateValue();
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
        GlobalEvent.OnCostReduction.RemoveListener(UpdateValue);
        Level = 0;
        _gameObject.SetActive(false);
    }
}