using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeUpgrade
{
    Pet,Job,Click
}

public class UpgradesBase : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private double _price;
    [SerializeField] private Button _button;
    [SerializeField] private double _increaseValue;
    [SerializeField] private TypeUpgrade _type;

    public void PurchasedUpgrade()
    {
        if(IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Money -= _price;
            Upgrade();
        }
    }

    public void Upgrade()
    {
        switch (_type)
        {
            case TypeUpgrade.Pet:
                break;
            case TypeUpgrade.Job:
                Modifier.UpgradeJob(id, _increaseValue);
                break;
            case TypeUpgrade.Click:
                break;
        }
        gameObject.SetActive(false);
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