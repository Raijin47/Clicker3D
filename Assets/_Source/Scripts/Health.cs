using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

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

    public void Init()
    {
        UpdateMaxHealth();
        _textMaxHealth = ConvertNumber.Convert(_maxHealth);
        CurrentHealth = YandexGame.savesData.CurrentHealth;
        GlobalEvent.OnHealthReduction.AddListener(HealthReduction);
    }

    public double CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth >= _maxHealth) 
            {
                while(_currentHealth >= _maxHealth)
                {
                    _stage.CurrentStage++;
                    _currentHealth -= _maxHealth;
                    UpdateMaxHealth();
                }
                _textMaxHealth = ConvertNumber.Convert(_maxHealth);
            }
            UpdateUI();
            YandexGame.savesData.CurrentHealth = _currentHealth;
        }
    }

    private void HealthReduction()
    {
        UpdateMaxHealth();
        _textMaxHealth = ConvertNumber.Convert(_maxHealth);
    }

    private void UpdateUI()
    {
        _imageHealth.fillAmount = (float)(_currentHealth / _maxHealth);
        _textHealth.text = ConvertNumber.Convert(_currentHealth) + " / " + _textMaxHealth;
    }

    public void OnReset()
    {
        CurrentHealth = 0d;
        UpdateMaxHealth();
    }

    private void UpdateMaxHealth()
    {
        _maxHealth = IncreaseValue.Calculate(_stage.CurrentStage, _baseHealth, _degreeIncreaseHealth) * Modifier.HealthReductionModifier;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _stage ??= GetComponent<Stage>();
    }
#endif
}