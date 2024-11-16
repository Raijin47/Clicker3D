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
    [SerializeField] private ImprovementManager _improvementManager;
    [SerializeField] private CountUpgradeButton _countUpgradeMoney;
    [SerializeField] private CountUpgradeButton _countUpgradeLove;
    [SerializeField] private RewardPanel _rewardPanel;

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
    public JobsManager Jobs => _jobsManager;
    public PetsManager Pets => _petsManager;
    public ParticleUIService Particle => _particleUIService;
    public Click Click => _click;
    public CameraLookAt CameraLookAt => _cameraLookAt;
    public Camera Camera => _camera;
    public RebithManager Rebith => _rebithManager;
    public ImprovementManager Improvement => _improvementManager;
    public RewardPanel RewardPanel => _rewardPanel;
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
        _improvementManager ??= GetComponent<ImprovementManager>();
    }
#endif
    #endregion
}