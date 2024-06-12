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
    private const double _baseHealth = 100;
    private const double _degreeIncreaseHealth = 1.07d;

    private double _currentHealth;
    private double _maxHealth;
    private string _textMaxHealth;
    private const string _division = " / ";
    private const float _one = 1f;
    private const double _increasePercent = 5;
    private const double _increaseEveryLevel = 10;
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
                yield return null;
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
        _maxHealth =
            //IncreaseValue.CalculateConstant(_stage.CurrentStage, 0.3d, 1)
            IncreaseValue.CalculateDegree(_stage.CurrentStage, _baseHealth, _degreeIncreaseHealth)
            * Math.Pow(_increasePercent, Math.Floor(_stage.CurrentStage / _increaseEveryLevel))
            * Modifier.HealthReductionModifier;
        _textMaxHealth = ConvertNumber.Convert(_maxHealth);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _stage ??= GetComponent<Stage>();
    }
#endif
}