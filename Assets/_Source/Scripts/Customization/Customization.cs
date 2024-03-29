using UnityEngine;
using UnityEngine.UI;
using YG;
using Assets.SimpleLocalization;

public class Customization : MonoBehaviour
{
    [SerializeField] private GameObject[] _skins;
    [SerializeField] private GameObject _purchasedSkinInfo;
    [SerializeField] private GameObject _buySkinInfo;
    [SerializeField] private Button _skinButton;
    [SerializeField] private Sprite[] _skinIcons;
    [SerializeField] private Material _hairMaterial;
    [SerializeField] private Material _eyesMaterial;
    [SerializeField] private LocalizedText _nameText;
    [SerializeField] private LocalizedText _infoText;
    [SerializeField] private Image _skinImage;
    [SerializeField] private SkinColor[] _skinColors;
    [SerializeField] private string[] _skinNames;
    [SerializeField] private int _skinPrice;

    private int _currentSkinEquip;
    private int _currentHairColor;
    private int _currentEyesColor;
    private int _currentSkinID;

    private readonly int _shaderMainColor = Shader.PropertyToID("_MainColor");
    private readonly int _shaderHighlight = Shader.PropertyToID("_HighlightColor");
    private readonly string _infoEquip = "Equip";
    private readonly string _infoUnequip = "Unequip";

    public void Init()
    {
        _currentSkinID = YandexGame.savesData.CurrentSkinEquip;
        _currentHairColor = YandexGame.savesData.CurrentHairColor;
        _currentEyesColor = YandexGame.savesData.CurrentEyesColor;

        SetHairColor(_currentHairColor);
        SetEyesColor(_currentEyesColor);
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
        _skins[_currentSkinEquip].SetActive(false);
        _currentSkinEquip = _currentSkinID;
        _skins[_currentSkinEquip].SetActive(true);
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