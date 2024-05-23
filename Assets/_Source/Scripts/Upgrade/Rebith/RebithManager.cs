using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RebithManager : UpgradeManagerBase
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI[] _currentRewardText;
    [SerializeField] private TextMeshProUGUI _infoReductionStageText;
    [SerializeField] private GameObject[] _rebirth—onfirmationPanel;
    [SerializeField] private Button _rebithButton;
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private int _dayToRebith;

    [SerializeField] private double _baseRewardRebith;
    [SerializeField] private double _degreeIncreaseRewardRebith;

    private double _currentDegreeIncreaseRebith;
    private string _currentRewardString;

    public override void Init()
    {
        _currentDegreeIncreaseRebith = _degreeIncreaseRewardRebith;

        GlobalEvent.OnStageChange.AddListener(UpdateUI);
        GlobalEvent.OnReductionLostDays.AddListener(UpdateStageInfo);
        GlobalEvent.OnIncreaseRebithMultiplier.AddListener(UpdateRebithReward);

        base.Init();

        UpdateUI();
    }

    private void UpdateUI()
    {
        _rebithButton.interactable = _stage.CurrentStage > _dayToRebith;
        UpdateRebithReward();
        UpdateStageInfo();
    }

    public void RebithButton()
    {
        _wallet.Rebith += CalculateValue();
        _rebirth—onfirmationPanel[0].SetActive(false);
        _rebirth—onfirmationPanel[1].SetActive(false);
        GlobalEvent.SendRebith();

        if (_particle != null) _particle.Play();
    }

    public void AdditionalReward()
    {
        _wallet.Rebith += System.Math.Round(CalculateValue() * 1.5f);
        _rebirth—onfirmationPanel[0].SetActive(false);
        _rebirth—onfirmationPanel[1].SetActive(false);
        GlobalEvent.SendRebith();

        if (_particle != null) _particle.Play();
    }

    private double CalculateValue()
    {
        return System.Math.Round(IncreaseValue.Calculate(_stage.CurrentStage, _baseRewardRebith, _currentDegreeIncreaseRebith) * Modifier.PrestigeMultiplier);
    }

    private void UpdateRebithReward()
    {
        _currentRewardString = ConvertNumber.Convert(CalculateValue());
        _currentRewardText[0].text = _currentRewardString;
        _currentRewardText[1].text = _currentRewardString;
        _currentRewardText[2].text = ConvertNumber.Convert(CalculateValue() / 2);
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