using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _autoIncomeCanvas;
    [SerializeField] private CanvasGroup _autoPopularityCanvas;
    [SerializeField] private CanvasGroup _rebithCanvas;
    [SerializeField] private CanvasGroup _shopCanvas;
    [SerializeField] private CanvasGroup _upgradeCanvas;
    [SerializeField] private CanvasGroup _outfitCanvas;

    [SerializeField] private Image _autoIncomeButton;
    [SerializeField] private Image _autoPopularivyButton;
    [SerializeField] private Image _rebithButton;
    [SerializeField] private Image _shopButton;
    [SerializeField] private Image _upgradeButton;
    [SerializeField] private Image _outfitButton;

    [SerializeField] private RectTransform _autoIncomeRect;
    [SerializeField] private RectTransform _autoPopularityRect;
    [SerializeField] private RectTransform _rebithRect;
    [SerializeField] private RectTransform _shopRect;
    [SerializeField] private RectTransform _upgradeRect;
    [SerializeField] private RectTransform _outfitRect;

    private SelectionState _selectionState;

    private SelectionBase _autoIncome;
    private SelectionBase _autoPopularity;
    private SelectionBase _rebith;
    private SelectionBase _shop;
    private SelectionBase _upgrade;
    private SelectionBase _outfit;

    public void Init()
    {
        _autoIncome = new SelectionBase(_autoIncomeCanvas, _autoIncomeButton, _autoIncomeRect);
        _autoPopularity = new SelectionBase(_autoPopularityCanvas, _autoPopularivyButton, _autoPopularityRect);
        _shop = new SelectionBase(_shopCanvas, _shopButton, _shopRect);
        _rebith = new SelectionBase(_rebithCanvas, _rebithButton, _rebithRect);
        _upgrade = new SelectionBase(_upgradeCanvas, _upgradeButton, _upgradeRect);
        _outfit = new SelectionBase(_outfitCanvas, _outfitButton, _outfitRect);
        _selectionState = new SelectionState(_upgrade);
    }

    public void AutoIncomeButton() => _selectionState.Change(_autoIncome);
    public void AutoPopularityButton() => _selectionState.Change(_autoPopularity);
    public void RebithButton() => _selectionState.Change(_rebith);
    public void ShopButton() => _selectionState.Change(_shop);
    public void UpgradeButton() => _selectionState.Change(_upgrade);
    public void OutfitButton() => _selectionState.Change(_outfit);
}