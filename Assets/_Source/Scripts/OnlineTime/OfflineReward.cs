using ExampleYGDateTime;
using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class OfflineReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMoney;
    [SerializeField] private TextMeshProUGUI _textHeart;
    [SerializeField] private TextMeshProUGUI _textCurrentTimeOffline;
    [SerializeField] private TextMeshProUGUI _textMaxTimeOffline;
    [SerializeField] private Slider _slider;
    [SerializeField] private JobsManager _jobsManager;
    [SerializeField] private PetsManager _petsManager;
    [SerializeField] private GameObject _panelReward;
    [SerializeField] private DailyReward _dailyReward;
    private OfflineRewardService _offlineRewardService;
    private WaitForSeconds _interval = new WaitForSeconds(10);
    private bool _isInitOfflineTimer;
    private double _offlineRewardMoney;
    private double _offlineRewardHeart;

    private void Awake()
    {
#if UNITY_EDITOR
            _offlineRewardService = new OfflineRewardUnityService();
#else
        _offlineRewardService = new OfflineRewardYandexService();
#endif       
    }

    public void Init()
    {
        _isInitOfflineTimer = YG.YandexGame.savesData.IsInitOfflineTimer;
        _offlineRewardService.onCompletedInitOfflineTimer += OnCompletedInitialize;
        _offlineRewardService.InitializeOfflineTimer().Forget();
    }

    private void OnCompletedInitialize()
    {
        if (_isInitOfflineTimer && _jobsManager.CurrentIncome != 0 | _petsManager.CurrentIncome != 0)
        {
            double OfflineTime = Mathf.Clamp(_offlineRewardService.OfflineTime, 0, (int)Modifier.OfflineIncomeTime);
            _offlineRewardMoney = Math.Round(_jobsManager.CurrentIncome * Modifier.OfflineIncomeModifire * OfflineTime);
            _offlineRewardHeart = Math.Round(_petsManager.CurrentIncome * Modifier.OfflineIncomeModifire * OfflineTime);
            _textMoney.text = ConvertNumber.Convert(_offlineRewardMoney);
            _textHeart.text = ConvertNumber.Convert(_offlineRewardHeart);

            TimeSpan currentOfflineSpan = TimeSpan.FromSeconds(OfflineTime);
            TimeSpan maxOfflineSpan = TimeSpan.FromSeconds(Modifier.OfflineIncomeTime);

            _textCurrentTimeOffline.text = currentOfflineSpan.Hours + "H " + currentOfflineSpan.Minutes + "M";
            _textMaxTimeOffline.text = maxOfflineSpan.Hours + "H " + maxOfflineSpan.Minutes + "M";

            _slider.maxValue = (float)Modifier.OfflineIncomeTime;

            _slider.value = (float)OfflineTime;

            _panelReward.SetActive(true);
        }
        else
        {
            YG.YandexGame.savesData.IsInitOfflineTimer = true;
            _dailyReward.Init();
        }

        StartCoroutine(SaveTimeCoroutine());
    }

    public void GetReward(bool isAdsBoost)
    {
        if (isAdsBoost)
        {
            _offlineRewardMoney *= 2;
            _offlineRewardHeart *= 2;
        }

        Locator.Instance.Wallet.Money += _offlineRewardMoney;
        Locator.Instance.Health.CurrentHealth += _offlineRewardHeart;

        _panelReward.SetActive(false);
        _dailyReward.Init();
    }

    private IEnumerator SaveTimeCoroutine()
    {
        while(true)
        {
            SaveTime();
            yield return _interval;
        }
    }

    private async void SaveTime()
    {
        await _offlineRewardService.SaveOfflineTime();
    }
}