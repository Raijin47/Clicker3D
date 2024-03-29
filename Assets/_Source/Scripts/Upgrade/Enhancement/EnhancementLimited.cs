using UnityEngine;

public abstract class EnhancementLimited : EnhancementBase
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
            _effectText.text = _currentValue + "%";
        }
        else
        {
            _priceText.text = ConvertNumber.Convert(_currentPrice);
            _effectText.text = _currentValue + "% > " + _nextValue + "%";
        }
    }

    protected override bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Money >= _currentPrice && Level != _maxLevel;
        return _isPurchaseAvailable;
    }
}