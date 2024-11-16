using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using System;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Image _imageHealth;
    [SerializeField] private TextMeshProUGUI _textHealth;
    private const double _baseHealth = 45;
    private const double _degreeIncreaseHealth = 1.15d;

    private double _currentHealth;
    private double _maxHealth;
    private string _textMaxHealth;
    private const string _division = " / ";
    private const float _one = 1f;
    private Coroutine _maxHealthUpdateProcessCoroutine;


    public void Init()
    {
        UpdateMaxHealth();
        CurrentHealth = YandexGame.savesData.CurrentHealth;
        GlobalEvent.OnHealthReduction.AddListener(UpdateMaxHealth);

        if(_maxHealthUpdateProcessCoroutine != null)
        {
            StopCoroutine(_maxHealthUpdateProcessCoroutine);
            _maxHealthUpdateProcessCoroutine = null;
        }
        _maxHealthUpdateProcessCoroutine = StartCoroutine(MaxHealthUpdateProcess());
    }

    public double CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            UpdateUI();
            YandexGame.savesData.CurrentHealth = _currentHealth;
        }
    }

    private readonly WaitForSeconds interval = new(1f);

    private IEnumerator MaxHealthUpdateProcess()
    {
        while (true)
        {
            while (_currentHealth >= _maxHealth)
            {
                _stage.CurrentStage++;
                _currentHealth -= _maxHealth;
                UpdateMaxHealth();
                UpdateUI();
                YandexGame.savesData.CurrentHealth = _currentHealth;
                yield return interval;
            }
            yield return null;
        }
    }

    private void UpdateUI()
    {
        if(_currentHealth / _maxHealth > float.MaxValue) _imageHealth.fillAmount = _one;
        else _imageHealth.fillAmount = (float)(_currentHealth / _maxHealth);

        _textHealth.text = ConvertNumber.Convert(_currentHealth) + _division + _textMaxHealth;
    }

    public void OnReset()
    {
        UpdateMaxHealth();
        CurrentHealth = 0d;
    }

    private void UpdateMaxHealth()
    {
        _maxHealth = Math.Round(IncreaseValue.Calculate(_stage.CurrentStage, _baseHealth, _degreeIncreaseHealth)
            * Modifier.HealthReductionModifier * ((_stage.CurrentStage % 5 == 0) ? 3 : 1) * ((_stage.CurrentStage % 10 == 0) ? 2 : 1));
        _textMaxHealth = ConvertNumber.Convert(_maxHealth);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _stage ??= GetComponent<Stage>();
    }
#endif
}