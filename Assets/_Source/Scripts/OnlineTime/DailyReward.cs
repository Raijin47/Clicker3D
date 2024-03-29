using Assets.SimpleLocalization;
using ExampleYGDateTime;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateTimeText;
    [SerializeField] private TextMeshProUGUI _rewardAmountText;
    [SerializeField] private LocalizedDynamic _dailyLoginInRowText;
    [SerializeField] private GameObject _dailyRewardPanel;
    private DailyRewardService _dailyRewardService;
    private Coroutine _updateDateTimeCoroutine;
    private double _currentReward;

    private void Awake()
    {
#if UNITY_EDITOR
        _dailyRewardService = new DailyRewardUnityService();
#else
            _dailyRewardService = new DailyRewardYandexService();
#endif
    }

    public void Init()
    {
        _dailyRewardService.onCompletedGetDateTime += OnCompletedGetServerTime;
        _dailyRewardService.GetDateTimeServerYandex().Forget();
    }

    private void OnCompletedGetServerTime()
    {
        if(_dailyRewardService.IsNewDay())
        {
            YandexGame.savesData.IsClaimReward = false;
        }

        if(!YandexGame.savesData.IsClaimReward) OpenDailyRewardPanel();
    }

    private IEnumerator UpdateDateTimeCoroutine()
    {
        while(true)
        {
            _dateTimeText.text = _dailyRewardService.DateTime;
            yield return null;
        }
    }

    private void OpenDailyRewardPanel()
    {
        _dailyRewardPanel.SetActive(true);

        if (_dailyRewardService.IsDailyLogin) YandexGame.savesData.DailyLoginInRow++;
        else YandexGame.savesData.DailyLoginInRow = 1;

        _dailyLoginInRowText.SetValue(YandexGame.savesData.DailyLoginInRow.ToString());

        _currentReward = Mathf.Clamp(100 * YandexGame.savesData.DailyLoginInRow, 100, 1000);
        _rewardAmountText.text = _currentReward.ToString();
    }

    public async void GetReward()
    {
        _dailyRewardPanel.SetActive(false);
        Locator.Instance.Wallet.Diamonds += _currentReward;
        YandexGame.savesData.IsClaimReward = true;
        await _dailyRewardService.SetDate();
    }

    public void StartDateTimeCoroutine()
    {
        if(_updateDateTimeCoroutine != null)
        {
            StopCoroutine(_updateDateTimeCoroutine);
            _updateDateTimeCoroutine = null;
        }
        _updateDateTimeCoroutine = StartCoroutine(UpdateDateTimeCoroutine());
    }

    public void StopDateTimeCoroutine()
    {
        StopCoroutine(_updateDateTimeCoroutine);
        _updateDateTimeCoroutine = null;
    }
}