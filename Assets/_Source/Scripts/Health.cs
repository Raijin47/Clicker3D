using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Image _imageHealth;
    [SerializeField] private TextMeshProUGUI _textHealth;
    [SerializeField] private double _baseHealth;
    [SerializeField] private double _degreeIncreaseHealth;

    private double _currentHealth;
    private double _maxHealth;
    private string _textMaxHealth;
    private readonly string _division = " / ";
    private const double _additionalBaseHealth = 99;
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
        _maxHealth = System.Math.Round((System.Math.Pow(_baseHealth, _stage.CurrentStage * _degreeIncreaseHealth) + _additionalBaseHealth) * Modifier.HealthReductionModifier);
        //_maxHealth = IncreaseValue.Calculate(_stage.CurrentStage, _baseHealth, _degreeIncreaseHealth) * Modifier.HealthReductionModifier;
        _textMaxHealth = ConvertNumber.Convert(_maxHealth);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _stage ??= GetComponent<Stage>();
    }
#endif
}