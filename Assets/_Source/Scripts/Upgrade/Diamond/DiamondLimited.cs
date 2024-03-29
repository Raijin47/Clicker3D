using UnityEngine;

public abstract class DiamondLimited : DiamondBase
{
    [SerializeField] private GameObject _textProcess;
    [SerializeField] private GameObject _textMax;

    [SerializeField] private int _maxLevel;

    protected override void UpdateUI()
    {
        if (Level == _maxLevel)
        {
            _textProcess.SetActive(false);
            _textMax.SetActive(true);
            UpdateTextMax();
        }
        else
        {
            _priceText.text = ConvertNumber.Convert(_currentPrice);
            UpdateTextProcess();
        }
    }

    protected abstract void UpdateTextMax();
    protected abstract void UpdateTextProcess();

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Diamonds >= _currentPrice && Level != _maxLevel;
        return _isPurchaseAvailable;
    }
}