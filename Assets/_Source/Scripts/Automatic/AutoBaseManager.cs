using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AutoBaseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buyPriceText;
    [SerializeField] private TextMeshProUGUI _currentIncomeText;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] protected AutoBase[] _autoBases;
    [SerializeField] private double[] _price;
    private double _currentPrice;
    protected int _id;
    private Timer _timer;
    protected double _income;
    private Coroutine _updateIncomeCoroutine;

    public AutoBase[] AutoBases => _autoBases;
    public double CurrentIncome => _income;

    public void Init(int id)
    {
        _timer = new Timer(1f);
        _id = id;

        UpdatePrice();

        for (int i = 0; i < _id; i++) Activate(i);

        CalculateIncome();

        GlobalEvent.OnRebith.AddListener(OnReset);
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
        AddAdditionalListener();
        CheckInteractableButton();
    }

    private IEnumerator IncomeProcess()
    {
        while(true)
        {
            _timer.Update();
            if(_timer.IsCompleted)
            {
                GetIncome();
                _timer.RestartTimer();
            }
            yield return null;
        }
    }

    protected abstract void AddAdditionalListener();
    protected abstract void GetIncome();
    protected abstract void Activate(int i);
    protected abstract void SaveAuto();

    public void BuyButton()
    {
        if (IsPurchaseAvailable())
        {
            _autoBases[_id].Activate(1);
            _id++;
            _wallet.Money -= _currentPrice;
            SaveAuto();
            UpdatePrice();
            CalculateIncome();
            Locator.Instance.Particle.GoldTransform.position = _buttonBuy.transform.position;
            Locator.Instance.Particle.GoldParticle.Play();
        }    
    }

    protected void OnReset()
    {
        for (int i = 0; i < _autoBases.Length; i++) Deactivate(i);
        _id = 0;
        CalculateIncome();
        SaveAuto();
        UpdatePrice();
    }

    protected void Deactivate(int i)
    {
        _autoBases[i].Deactivate();
    }

    public virtual void CalculateIncome()
    {
        if (_updateIncomeCoroutine != null)
        {
            StopCoroutine(_updateIncomeCoroutine);
            _updateIncomeCoroutine = null;
        }

        _income = 0;

        for(int i = 0; i < _autoBases.Length; i++)
        {
            _income += _autoBases[i].CurrentIncome;
        }

        _currentIncomeText.text = "+" + ConvertNumber.Convert(_income);
        _updateIncomeCoroutine = StartCoroutine(IncomeProcess());
    }

    protected void RecalculateAutoBase()
    {
        for(int i = 0; i < _autoBases.Length; i++)
        {
            _autoBases[i].GetCurrentIncome();
        }
        CalculateIncome();
    }

    private void CheckInteractableButton()
    {
        _buttonBuy.interactable = IsPurchaseAvailable();
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _currentPrice;
        return _isPurchaseAvailable;
    }

    private void UpdatePrice()
    {
        _buyPanel.SetActive(_id < _autoBases.Length);

        _currentPrice = _price[_id];
        _buyPriceText.text = ConvertNumber.Convert(_currentPrice);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _wallet ??= GetComponent<Wallet>();
    }
#endif
}