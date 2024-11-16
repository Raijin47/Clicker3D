using UnityEngine;
using UnityEngine.UI;
using YG;

public class SelectionManager : MonoBehaviour
{
    #region Component
    [SerializeField] private Button _incomeButton;
    [SerializeField] private Button _petButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _outfitButton;
    [SerializeField] private Button _rebithButton;
    [SerializeField] private Button _shopButton;

    [SerializeField] private CanvasGroup _incomeCanvas;
    [SerializeField] private CanvasGroup _petCanvas;
    [SerializeField] private CanvasGroup _rebithCanvas;
    [SerializeField] private CanvasGroup _shopCanvas;
    [SerializeField] private CanvasGroup _upgradeCanvas;
    [SerializeField] private CanvasGroup _outfitCanvas;

    [SerializeField] private Image _incomeImage;
    [SerializeField] private Image _petImage;
    [SerializeField] private Image _rebithImage;
    [SerializeField] private Image _shopImage;
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private Image _outfitImage;

    [SerializeField] private RectTransform _incomeRect;
    [SerializeField] private RectTransform _petRect;
    [SerializeField] private RectTransform _rebithRect;
    [SerializeField] private RectTransform _shopRect;
    [SerializeField] private RectTransform _upgradeRect;
    [SerializeField] private RectTransform _outfitRect;

    private SelectionState _selectionState;

    private SelectionBase _income;
    private SelectionBase _pet;
    private SelectionBase _rebith;
    private SelectionBase _shop;
    private SelectionBase _upgrade;
    private SelectionBase _outfit;
    #endregion


    private void Start()
    {
        _income = new SelectionBase(_incomeCanvas, _incomeImage, _incomeRect);
        _pet = new SelectionBase(_petCanvas, _petImage, _petRect);
        _shop = new SelectionBase(_shopCanvas, _shopImage, _shopRect);
        _rebith = new SelectionBase(_rebithCanvas, _rebithImage, _rebithRect);
        _upgrade = new SelectionBase(_upgradeCanvas, _upgradeImage, _upgradeRect);
        _outfit = new SelectionBase(_outfitCanvas, _outfitImage, _outfitRect);
        _selectionState = new SelectionState(_income);

        _incomeButton.onClick.AddListener(OpenIncomePanel);
        _petButton.onClick.AddListener(OpenPetPanel);
        _upgradeButton.onClick.AddListener(OpenImprovedPanel);
        _outfitButton.onClick.AddListener(OpenOutfitPanel);
        _shopButton.onClick.AddListener(OpenShopPanel);
        _rebithButton.onClick.AddListener(OpenRebithPanel);
    }

    private int _id;
    public int ID => _id;


    public void Init()
    {
        _petButton.interactable = YandexGame.savesData.StepTutorial > 5;
        _upgradeButton.interactable = YandexGame.savesData.StepTutorial > 7;
        _outfitButton.interactable = YandexGame.savesData.StepTutorial > 10;
        _rebithButton.interactable = YandexGame.savesData.StepTutorial > 12;
        _shopButton.interactable = YandexGame.savesData.StepTutorial > 14;
    }

    private void OpenIncomePanel()
    {
        SFXController.OnPanelSwap?.Invoke();
        _selectionState.Change(_income);
        _id = 0;
    }
    private void OpenPetPanel()
    {
        SFXController.OnPanelSwap?.Invoke();
        _selectionState.Change(_pet);
        _id = 1;
    }
    private void OpenImprovedPanel()
    {
        SFXController.OnPanelSwap?.Invoke();
        _selectionState.Change(_upgrade);
        _id = 2;
    }
    private void OpenOutfitPanel()
    {
        SFXController.OnPanelSwap?.Invoke();
        _selectionState.Change(_outfit);
        _id = 3;
    }
    private void OpenRebithPanel()
    {
        SFXController.OnPanelSwap?.Invoke();
        _selectionState.Change(_rebith);
        _id = 4;
    }
    private void OpenShopPanel()
    {
        SFXController.OnPanelSwap?.Invoke();
        _selectionState.Change(_shop);
        _id = 5;
    } 
}