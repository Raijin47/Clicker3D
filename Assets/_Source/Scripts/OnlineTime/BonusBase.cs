using ExampleYGDateTime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BonusBase : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _timerObject;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] protected TextMeshProUGUI _textValue;
    [SerializeField] protected int _baseTime;

    private TimeBonusService _timeBonusService;
    private Coroutine _updateTimerCoroutine;

    private void Awake()
    {
#if UNITY_EDITOR
        _timeBonusService = new TimeBonusUnityService();
#else
        _timeBonusService = new TimeBonusYandexService();
#endif
    }

    public virtual void Init()
    {
        _timeBonusService.onCompletedInitBonusTimer += OnCompletedInitialize;
        _timeBonusService.InitializeTimerReceived(_id).Forget();
        UpdateBonusValue();
        GlobalEvent.OnChangeModifireBonusAds.AddListener(UpdateBonusValue);
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (_timeBonusService.IsActiveTimer)
        {
            _textTimer.text = _timeBonusService.TimeLeft();

            if (_timeBonusService.CheckTimerEnded())
            {
                ShowInfo();
                _timeBonusService.IsActiveTimer = false;
            }
            yield return null;
        }
    }

    private void OnCompletedInitialize(int id)
    {
        if (_timeBonusService.IsActiveTimer)
        {
            ShowTimer();
            StartTimerCoroutine();
        }
        else ShowInfo();
    }

    private void StartTimerCoroutine()
    {
        if (_updateTimerCoroutine != null)
        {
            StopCoroutine(_updateTimerCoroutine);
            _updateTimerCoroutine = null;
        }
        _updateTimerCoroutine = StartCoroutine(UpdateTimerCoroutine());
    }

    public virtual async void Reward()
    {
        await _timeBonusService.StartTimerReceived(GetTime());
        _timeBonusService.SetTimerData(_id);

        ShowTimer();
        StartTimerCoroutine();
    }

    protected virtual void ShowInfo()
    {
        _button.interactable = true;
        _infoPanel.SetActive(true);
        _timerObject.SetActive(false);
    }

    protected virtual void ShowTimer()
    {
        _button.interactable = false;
        _infoPanel.SetActive(false);
        _timerObject.SetActive(true);
    }

    protected abstract int GetTime();
    protected abstract void UpdateBonusValue();
}