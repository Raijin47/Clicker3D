using UnityEngine;
using System;

public class Click : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Camera _camera;
    [SerializeField] private MessageSystem _message;
    [SerializeField] private double _criticalModifier;
    [SerializeField] private ParticleSystem _particle;

    private double _clickForce, _criticalClickForce;
    private double _clickIncomeMoney, _criticalClickIncomeMoney;
    private double _clickIncomeLove, _criticalClickIncomeLove;
    private string _textClick, _criticalTextClick;

    public double ClickIncome => _clickIncomeMoney;

    public void Init()
    {
        GlobalEvent.OnIncreaseClick.AddListener(UpdateValue);
        UpdateValue();
    }

    public void ClickButton()
    {
        _particle.Play();

        if(IsCritical())
        {
            _message.CriticalClick(_criticalTextClick);
            _health.CurrentHealth += _criticalClickIncomeLove;
            _wallet.Money += _criticalClickIncomeMoney;
        }
        else
        {
            _message.NormalClick(_textClick);
            _health.CurrentHealth += _clickIncomeLove;
            _wallet.Money += _clickIncomeMoney;
        }
    }

    private bool IsCritical()
    {
        return Modifier.EnhancementCritChanceClick > UnityEngine.Random.value;
    }

    private void UpdateValue()
    {
        _clickForce = Math.Round(Modifier.DiamondIncome * Modifier.EnhancementClickForce * Modifier.PrestigeClickForce * Modifier.ADsBoost);
        _clickIncomeMoney = Math.Round(_clickForce * Modifier.TimeMoneyBoost);
        _clickIncomeLove = Math.Round(_clickForce * Modifier.TimeLoveBoost);
        _textClick = ConvertNumber.Convert(_clickForce);

        _criticalClickForce = Math.Round(_clickForce * _criticalModifier);
        _criticalClickIncomeMoney = Math.Round(_criticalClickForce * Modifier.TimeMoneyBoost);
        _criticalClickIncomeLove = Math.Round(_criticalClickForce * Modifier.TimeLoveBoost);
        _criticalTextClick = ConvertNumber.Convert(_criticalClickForce);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _health ??= GetComponent<Health>();
        _wallet ??= GetComponent<Wallet>();
        _message ??= GetComponent<MessageSystem>();
    }
#endif
}