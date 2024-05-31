using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesBase : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private double _price;
    [SerializeField] private Button _button;
    [SerializeField] private double _increaseValue;
    [SerializeField] private GameObject _upgradeObject;
    [SerializeField] private AutoBase _targetUpgrade;

    private bool _isShow;

    public bool IsShow => _isShow;
    public bool IsActive
    {
        get => YG.YandexGame.savesData.IsUpgradeActive[id];
        set
        {
            YG.YandexGame.savesData.IsUpgradeActive[id] = value;
        }
    }

    public void PurchasedUpgrade()
    {
        if(IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _price;
            Activate();
        }
    }

    private void OnEnable()
    {
        GlobalEvent.OnMoneyChange.AddListener(CheckInteractableButton);
    }

    private void OnDisable()
    {
        GlobalEvent.OnMoneyChange.RemoveListener(CheckInteractableButton);
    }

    public void Show()
    {
        _upgradeObject.SetActive(true);
        _isShow = true;
        if (IsActive) Activate();
    }

    public void Activate()
    {
        IsActive = true;
        _isShow = true;
        _targetUpgrade.GetUpgrade(_increaseValue);
        _upgradeObject.SetActive(false);
    }

    public void Deactivate()
    {
        IsActive = false;
        _isShow = false;
        _upgradeObject.SetActive(false);
    }

    private void CheckInteractableButton()
    {
        _button.interactable = IsPurchaseAvailable();
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _price;
        return _isPurchaseAvailable;
    }
}