using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class UpgradeBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] protected int _id;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] protected TextMeshProUGUI _priceText;
    [SerializeField] protected TextMeshProUGUI _effectText;
    [SerializeField] protected Button _upgradeButton;

    [SerializeField] protected double _baseUpgradePrice;
    [SerializeField] private double _degreeIncreasePrice;
    [SerializeField] protected double _baseValue;
    [SerializeField] protected double _fixedIncreaseValue;

    private Coroutine _upgradeProcessCoroutine;
    private WaitForSeconds _intervalToUpgrade = new WaitForSeconds(.5f);

    protected double _currentValue;
    protected double _currentPrice;
    protected double _nextValue;
    private int _level;

    public int Level 
    { 
        protected get => _level;
        set 
        {
            _level = value;
            _levelText.text = _level.ToString();
        } 
    }

    public void Init()
    {
        Level = GetLevel();
        AddListener();
        UpdateValue();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPurchaseAvailable())
        {
            Upgrade();
            PlayParticle();
            _upgradeProcessCoroutine = StartCoroutine(UpgradeProcess());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_upgradeProcessCoroutine != null)
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
        Level++;
        ExecutePurchase();
        UpdateValue();
    }

    protected abstract void PlayParticle();

    protected void CheckInteractableButton()
    {
        _upgradeButton.interactable = IsPurchaseAvailable();
    }

    protected void UpdateValue()
    {
        SetLevel();
        _currentValue = CalculateUpgradeValue();
        _nextValue = CalculateUpgradeNext();
        _currentPrice = CalculateUpgradePrice();
        
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

    protected virtual double CalculateUpgradePrice()
    {
        return IncreaseValue.Calculate(_level, _baseUpgradePrice, _degreeIncreasePrice);
    }

    protected abstract void Execute();
    protected abstract void UpdateUI();
    protected abstract int GetLevel();
    protected abstract void AddListener();
    protected abstract void ExecutePurchase();
    protected abstract bool IsPurchaseAvailable();
    protected abstract void SetLevel();
}
