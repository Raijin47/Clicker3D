using UnityEngine;
using YG;
public class Locator : MonoBehaviour
{
    public static Locator Instance;

    [SerializeField] private Wallet _wallet;
    [SerializeField] private JobsManager _jobsManager;
    [SerializeField] private PetsManager _petsManager;
    [SerializeField] private Stage _stage;
    [SerializeField] private Health _health;
    [SerializeField] private ParticleUIService _particleUIService;
    [SerializeField] private Click _click;
    [SerializeField] private CameraLookAt _cameraLookAt;
    [SerializeField] private Bootstrap _bootstrap;
    [SerializeField] private Camera _camera;
    [SerializeField] private RebithManager _rebithManager;
    [SerializeField] private UpgradesManager _upgradesManager;
    [SerializeField] private CountUpgradeButton _countUpgradeMoney;
    [SerializeField] private CountUpgradeButton _countUpgradeLove;
    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
    private void Awake()
    {
        if (YandexGame.SDKEnabled) GetData();      
    }
    private void GetData()
    {
        if (Instance == null) Instance = this;
        _bootstrap.Init();
    }

    public Stage Stage => _stage;
    public Health Health => _health;
    public Wallet Wallet => _wallet;
    public JobsManager JobsManager => _jobsManager;
    public PetsManager PetsManager => _petsManager;
    public ParticleUIService Particle => _particleUIService;
    public Click Click => _click;
    public CameraLookAt CameraLookAt => _cameraLookAt;
    public Camera Camera => _camera;
    public RebithManager RebithManager => _rebithManager;
    public UpgradesManager UpgradesManager => _upgradesManager;

    public CountUpgradeButton CountLoveUpgrade => _countUpgradeLove;
    public CountUpgradeButton CountMoneyUpgrade => _countUpgradeMoney;

    #region OnValidate
#if UNITY_EDITOR
    private void OnApplicationQuit() => YandexGame.SaveProgress();
    private void OnValidate()
    {
        _wallet ??= GetComponent<Wallet>();
        _click ??= GetComponent<Click>();
        _stage ??= GetComponent<Stage>();
        _health ??= GetComponent<Health>();
        _jobsManager ??= GetComponent<JobsManager>();
        _petsManager ??= GetComponent<PetsManager>();
        _particleUIService ??= GetComponent<ParticleUIService>();
        _bootstrap ??= GetComponent<Bootstrap>();
        _rebithManager ??= GetComponent<RebithManager>();
        _upgradesManager ??= GetComponent<UpgradesManager>();
    }
#endif
    #endregion
}