using UnityEngine;
using UnityEngine.UI;
using YG;
using Assets.SimpleLocalization;

public class Customization : MonoBehaviour
{
    [SerializeField] private Outfit[] _outfits;
    [SerializeField] private GameObject _purchasedSkinInfo;
    [SerializeField] private GameObject _buySkinInfo;
    [SerializeField] private Button _skinButton;
    [SerializeField] private Sprite[] _skinIcons;
    [SerializeField] private Material _hairMaterial;
    [SerializeField] private Material _eyesMaterial;
    [SerializeField] private Material _bodyMaterial;
    [SerializeField] private LocalizedText _nameText;
    [SerializeField] private LocalizedText _infoText;
    [SerializeField] private Image _skinImage;
    [SerializeField] private SkinColor[] _skinColors;
    [SerializeField] private string[] _skinNames;
    [SerializeField] private int _skinPrice;

    [SerializeField] private Texture[] _bodyTextures;

    private int _currentSkinEquip;
    private int _currentHairColor;
    private int _currentEyesColor;
    private int _currentBodyColor;
    private int _currentSkinID;

    private readonly int _shaderMainTexture = Shader.PropertyToID("_MainTex");
    private readonly int _shaderMainColor = Shader.PropertyToID("_MainColor");
    private readonly int _shaderHighlight = Shader.PropertyToID("_HighlightColor");
    private readonly string _infoEquip = "Equip";
    private readonly string _infoUnequip = "Unequip";

    public void Init()
    {
        _currentSkinID = YandexGame.savesData.CurrentSkinEquip;
        _currentHairColor = YandexGame.savesData.CurrentHairColor;
        _currentEyesColor = YandexGame.savesData.CurrentEyesColor;
        _currentBodyColor = YandexGame.savesData.CurrentBodyColor;

        SetHairColor(_currentHairColor);
        SetEyesColor(_currentEyesColor);
        SetBodyColor(_currentBodyColor);
        SetSkin();
        UpdateUI();

        for (int i = 0; i < _skinColors.Length; i++) _skinColors[i].Init();
    }

    public void ButtonSkin()
    {
        if (YandexGame.savesData.SkinsPurchased[_currentSkinID]) SetSkin();
        else BuySkin();
        UpdateUI();
    }

    private void SetSkin() 
    {
        _outfits[_currentSkinEquip].Skin.SetActive(false);
        _currentSkinEquip = _currentSkinID;
        _outfits[_currentSkinEquip].Skin.SetActive(true);

        _bodyMaterial.SetTexture(_shaderMainTexture, _bodyTextures[_outfits[_currentSkinEquip].BodyType]);
        YandexGame.savesData.CurrentSkinEquip = _currentSkinID;
    }

    private void BuySkin()
    {
        if (IsPurchaseAvailable())
        {
            Locator.Instance.Wallet.Diamonds -= _skinPrice;
            YandexGame.savesData.SkinsPurchased[_currentSkinID] = true;
        }
    }

    public void SetHairColor(int id)
    {
        _currentHairColor = id;
        _hairMaterial.SetColor(_shaderMainColor, _skinColors[_currentHairColor].Color);
        YandexGame.savesData.CurrentHairColor = id;
    }

    public void SetBodyColor(int id)
    {
        _currentBodyColor = id;
        //_bodyMaterial.SetColor(_shaderMainColor, _)
        YandexGame.savesData.CurrentBodyColor = id;
    }

    public void SetEyesColor(int id)
    {
        _currentEyesColor = id;
        _eyesMaterial.SetColor(_shaderHighlight, _skinColors[_currentEyesColor].Color);
        YandexGame.savesData.CurrentEyesColor = id;
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

        if (YandexGame.savesData.SkinsPurchased[_currentSkinID])
        {
            _buySkinInfo.SetActive(false);
            _purchasedSkinInfo.SetActive(true);

            if (_currentSkinEquip == _currentSkinID)
            {
                _skinButton.interactable = false;
                _infoText.SetKey(_infoEquip);
            }
            else
            {
                _skinButton.interactable = true;
                _infoText.SetKey(_infoUnequip);
            }

            _skinImage.color = Color.white;
        }
        else
        {
            _buySkinInfo.SetActive(true);
            _purchasedSkinInfo.SetActive(false);
            _skinButton.interactable = IsPurchaseAvailable();

            _skinImage.color = Color.black; 
        }
    }

    private bool IsPurchaseAvailable()
    {
        bool _isPurchaseAvailable = Locator.Instance.Wallet.Diamonds >= _skinPrice;
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