using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.SimpleLocalization;
using System.Collections;

public class RebithManager : UpgradeManagerBase
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI[] _currentRewardText;
    [SerializeField] private TextMeshProUGUI _infoReductionStageText;
    [SerializeField] private TextMeshProUGUI _prestigeText;
    [SerializeField] private GameObject[] _rebirth—onfirmationPanel;
    [SerializeField] private Button _rebithButton;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private GameObject _lockImage;

    private const int DayToRebith = 59;
    private const double BaseReward = 0.05d;
    private const double IgnoreLevel = 35d;
    private const double Degree = 2d;

    private double _rewardValue;

    private string _currentRewardString;

    private const string PrestigeKey = "Prestige";

    public override void Init()
    {
        GlobalEvent.OnStageChange.AddListener(UpdateUI);
        GlobalEvent.OnReductionLostDays.AddListener(UpdateStageInfo);
        GlobalEvent.OnIncreaseRebithMultiplier.AddListener(UpdateRebithReward);

        base.Init();

        UpdateUI();
    }

    private void OnEnable()
    {
        LocalizationManager.LocalizationChanged += UpdateUI;
    }

    private void OnDisable()
    {
        LocalizationManager.LocalizationChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        bool isActive = _stage.CurrentStage > DayToRebith;
        _rebithButton.interactable = isActive;

        string text = LocalizationManager.Localize(PrestigeKey);

        _prestigeText.text = isActive ? TextUtility.GetBlackText(text) : TextUtility.GetWhiteText(text);
        UpdateRebithReward();
        UpdateStageInfo();
    }

    public void RebithButton()
    {
        _rewardValue = CalculateValue();
        _wallet.Rebith += _rewardValue;
        ExecuteRebith();
    }

    public void AdditionalReward()
    {
        _rewardValue = System.Math.Round(CalculateValue() * 1.5f);
        _wallet.Rebith += _rewardValue;
        ExecuteRebith();
    }

    private void ExecuteRebith()
    {
        _rebirth—onfirmationPanel[0].SetActive(false);
        _rebirth—onfirmationPanel[1].SetActive(false);
        Locator.Instance.Improvement.OnReset();
        GlobalEvent.SendRebith();

        StartCoroutine(PlayRebithProcess());
    }
    private readonly WaitForSeconds Interval = new(2f);
    private IEnumerator PlayRebithProcess()
    {
        if (_particle != null) _particle.Play();

        SFXController.OnRebithProcess?.Invoke();
        yield return Interval;

        Locator.Instance.RewardPanel.OpenPanel(2,_rewardValue);
    }

    private double CalculateValue()
    {
        double Stage = System.Math.Clamp(_stage.CurrentStage - IgnoreLevel, 0, int.MaxValue);

        return System.Math.Round(IncreaseValue.CalculateDegree(Stage, BaseReward, Degree) * Modifier.PrestigeMultiplier);
    }

    private void UpdateRebithReward()
    {
        _currentRewardString = ConvertNumber.Convert(CalculateValue());
        _currentRewardText[0].text = _currentRewardString;
        _currentRewardText[1].text = _currentRewardString;
        _currentRewardText[2].text = ConvertNumber.Convert(System.Math.Round(CalculateValue() / 2));
    }

    private void UpdateStageInfo()
    {
        int Stage = (int)(_stage.CurrentStage * Modifier.ReductionLostDays);
        if (Stage == 0) Stage = 1;

        _infoReductionStageText.text = Stage.ToString();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _stage ??= GetComponent<Stage>();
        _wallet ??= GetComponent<Wallet>();
    }
#endif
}