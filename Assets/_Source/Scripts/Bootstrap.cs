using UnityEngine;
using YG;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SelectionManager _selectionManager;
    [SerializeField] private DiamondManager _diamondManager;
    [SerializeField] private EnhancementManager _enhancementManager;
    [SerializeField] private LanguageSettings _languageSettings;
    [SerializeField] private OfflineReward _offlineReward;
    [SerializeField] private AdsManager _adsManager;
    [SerializeField] private Customization _customization;

    public void Init()
    {
        _adsManager.Init();
        _languageSettings.Init();
        Locator.Instance.RebithManager.Init();
        _diamondManager.Init();
        _enhancementManager.Init();
        Locator.Instance.Wallet.Init();
        Locator.Instance.Click.Init();
        Locator.Instance.Stage.Init();
        Locator.Instance.Health.Init();
        Locator.Instance.JobsManager.Init(YandexGame.savesData.CurrentJob);
        Locator.Instance.PetsManager.Init(YandexGame.savesData.CurrentPet);
        _offlineReward.Init();
        _customization.Init();
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
        _enhancementManager ??= GetComponent<EnhancementManager>();
        _languageSettings ??= GetComponent<LanguageSettings>();
        _offlineReward ??= GetComponent<OfflineReward>();
        _adsManager ??= GetComponent<AdsManager>();
        _customization ??= GetComponent<Customization>();
    }
#endif
    #endregion
}