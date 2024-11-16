using UnityEngine;
using UnityEngine.UI;
using YG;
using Assets.SimpleLocalization;
using TMPro;

public class Customization : MonoBehaviour
{
    [SerializeField] private Outfit[] _outfits;
    [SerializeField] private Button _skinButton;
    [SerializeField] private Sprite[] _skinIcons;
    [SerializeField] private Material _hairMaterial;
    [SerializeField] private Material _eyesMaterial;
    [SerializeField] private Material _bodyMaterial;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private LocalizedText _nameText;
    [SerializeField] private Image _skinImage;
    [SerializeField] private SkinColor[] _skinColors;
    [SerializeField] private string[] _skinNames;
    [SerializeField] private Texture[] _bodyTextures;

    private const int SkinPrice = 1500;

    private int _currentSkinEquip;
    private int _currentHairColor;
    private int _currentEyesColor;
    private int _currentBodyColor;
    private int _currentSkinID;

    private readonly int _shaderMainTexture = Shader.PropertyToID("_MainTex");
    private readonly int _shaderMainColor = Shader.PropertyToID("_MainColor");
    private readonly int _shaderHighlight = Shader.PropertyToID("_HighlightColor");
    private const string Equip = "Equip";
    private const string Unequip = "Unequip";

    public void Init()
    {
        _currentSkinID = YandexGame.savesData.CurrentSkinEquip;
        _currentHairColor = YandexGame.savesData.CurrentHairColor;
        _currentEyesColor = YandexGame.savesData.CurrentEyesColor;
        _currentBodyColor = YandexGame.savesData.CurrentBodyColor;

        SetHairColor(_currentHairColor);
        SetEyesColor(_currentEyesColor);
        SetBodyColor(_currentBodyColor);
        SetOutfit();
        UpdateUI();

        for (int i = 0; i < _skinColors.Length; i++) _skinColors[i].Init();
    }

    public void ButtonOutfit()
    {
        if (YandexGame.savesData.SkinsPurchased[_currentSkinID]) SetOutfit();
        else BuyOutfit();
        UpdateUI();
    }

    private void SetOutfit() 
    {
        _outfits[_currentSkinEquip].Skin.SetActive(false);
        _currentSkinEquip = _currentSkinID;
        _outfits[_currentSkinEquip].Skin.SetActive(true);

        _bodyMaterial.SetTexture(_shaderMainTexture, _bodyTextures[_outfits[_currentSkinEquip].BodyType]);
        YandexGame.savesData.CurrentSkinEquip = _currentSkinID;

        SFXController.OnSelectOutfit?.Invoke();
    }

    private void OnEnable()
    {
        LocalizationManager.LocalizationChanged += UpdateUIState;
        GlobalEvent.OnDiamondChange.AddListener(UpdateUIState);
    }

    private void OnDisable()
    {
        LocalizationManager.LocalizationChanged -= UpdateUIState;
    }

    private void BuyOutfit()
    {
        if (IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Diamonds -= SkinPrice;
            YandexGame.savesData.SkinsPurchased[_currentSkinID] = true;
            SFXController.OnUpgradeDiamonds?.Invoke();
        }
    }

    public void SetHairColor(int id)
    {
        _currentHairColor = id;
        _hairMaterial.SetColor(_shaderMainColor, _skinColors[_currentHairColor].Color);
        YandexGame.savesData.CurrentHairColor = id;
        SFXController.OnSelectOutfit?.Invoke();
    }

    public void SetBodyColor(int id)
    {
        _currentBodyColor = id;
        _bodyMaterial.SetColor(_shaderMainColor, _skinColors[_currentBodyColor].Color);
        YandexGame.savesData.CurrentBodyColor = id;
        SFXController.OnSelectOutfit?.Invoke();
    }

    public void SetEyesColor(int id)
    {
        _currentEyesColor = id;
        _eyesMaterial.SetColor(_shaderHighlight, _skinColors[_currentEyesColor].Color);
        YandexGame.savesData.CurrentEyesColor = id;
        SFXController.OnSelectOutfit?.Invoke();
    }

    public void PreviewButton()
    {
        _currentSkinID--;
        if (_currentSkinID < 0)
            _currentSkinID = _skinIcons.Length - 1;

        UpdateUI();
    }

    public void NextButton()
    {
        _currentSkinID++;
        if (_currentSkinID >= _skinIcons.Length) 
            _currentSkinID = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _nameText.SetKey(_skinNames[_currentSkinID]);
        _skinImage.sprite = _skinIcons[_currentSkinID];

        UpdateUIState();
    }

    private void UpdateUIState()
    {
        if (YandexGame.savesData.SkinsPurchased[_currentSkinID]) Purchased();
        else NotPurchased();
    }

    private void Purchased()
    {
        if (_currentSkinEquip == _currentSkinID)
        {
            _skinButton.interactable = false;
            _buttonText.text = TextUtility.GetWhiteText(LocalizationManager.Localize(Equip));
        }
        else
        {
            _skinButton.interactable = true;
            _buttonText.text = TextUtility.GetBlackText(LocalizationManager.Localize(Unequip));
        }

        _skinImage.color = Color.white;
    }

    private void NotPurchased()
    {
        _buttonText.text = IsPurchaseAvailable() ?
            TextUtility.GetBlackText(TextUtility.DiamondImg + SkinPrice):
            TextUtility.GetWhiteText(TextUtility.DiamondImg + SkinPrice);

        _skinButton.interactable = IsPurchaseAvailable();

        _skinImage.color = Color.black;
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Diamonds >= SkinPrice;
        return _isPurchaseAvailable;
    }

    public void UnlockColor(int id)
    {
        _skinColors[id].Unlock();
    }
}

[System.Serializable]
public class Outfit
{
    [SerializeField] private GameObject _skin;
    [SerializeField] private int _bodyType;

    public GameObject Skin => _skin;
    public int BodyType => _bodyType;
}