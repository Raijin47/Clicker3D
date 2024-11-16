using UnityEngine;
using YG;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SelectionManager _selectionManager;
    [SerializeField] private DiamondManager _diamondManager;
    [SerializeField] private EnhancementForceClick _enhancementClick;
    [SerializeField] private LanguageSettings _languageSettings;
    [SerializeField] private OfflineReward _offlineReward;
    [SerializeField] private AdsManager _adsManager;
    [SerializeField] private Customization _customization;
    [SerializeField] private HidingPanel _hidingPanel;
    [SerializeField] private SFXController _sfxController;
    [SerializeField] private DailyRoulette _roulette;

    public void Init()
    {
        _languageSettings.Init();
        _adsManager.Init();
        Locator.Instance.Wallet.Init();
        Locator.Instance.Rebith.Init();
        _diamondManager.Init();
        Locator.Instance.Improvement.Init();
        _enhancementClick.Init();
        Locator.Instance.Click.Init();
        Locator.Instance.Stage.Init();
        Locator.Instance.Health.Init();
        Locator.Instance.Jobs.Init(YandexGame.savesData.CurrentJob);
        Locator.Instance.Pets.Init(YandexGame.savesData.CurrentPet);
        
        _hidingPanel.Init();
        _offlineReward.Init();
        _roulette.Init();
        _customization.Init();
        _sfxController.Init();
    }

    private void Start()
    {
        _selectionManager.Init();
    }

    #region OnValidate
#if UNITY_EDITOR
    private void OnValidate()
    {
        _selectionManager ??= GetComponent<SelectionManager>();
        _diamondManager ??= GetComponent<DiamondManager>();
        _languageSettings ??= GetComponent<LanguageSettings>();
        _offlineReward ??= GetComponent<OfflineReward>();
        _adsManager ??= GetComponent<AdsManager>();
        _customization ??= GetComponent<Customization>();
        _roulette ??= GetComponent<DailyRoulette>();
    }
#endif
    #endregion
}