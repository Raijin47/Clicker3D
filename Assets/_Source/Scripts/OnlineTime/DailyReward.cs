using Assets.SimpleLocalization;
using ExampleYGDateTime;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateTimeText;
    private DailyRewardService _dailyRewardService;
    private Coroutine _updateDateTimeCoroutine;

    private void Awake()
    {
#if UNITY_EDITOR
        _dailyRewardService = new DailyRewardUnityService();
#else
            _dailyRewardService = new DailyRewardYandexService();
#endif
    }

    private IEnumerator UpdateDateTimeCoroutine()
    {
        while(true)
        {
            _dateTimeText.text = _dailyRewardService.DateTime;
            yield return null;
        }
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