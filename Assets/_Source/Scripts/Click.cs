using UnityEngine;
using System;

public class Click : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Camera _camera;
    [SerializeField] private MessageSystem _message;
    [SerializeField] private double _criticalModifier;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private RectTransform _particleTransform;

    private double _clickForce, _criticalClickForce;
    private string _textClick, _criticalTextClick;

    public double ClickIncome => _clickForce;

    public void Init()
    {
        GlobalEvent.OnIncreaseClick.AddListener(UpdateValue);
        UpdateValue();
    }

    public void ClickButton()
    {
        _particleTransform.position = Input.mousePosition;
        _particle.Play();

        if(IsCritical())
        {
            _message.CriticalClick(_criticalTextClick);
            _wallet.Money += _criticalClickForce;
        }
        else
        {
            _message.NormalClick(_textClick);
            _wallet.Money += _clickForce;
        }
    }

    private bool IsCritical()
    {
        return Modifier.EnhancementCritChanceClick > UnityEngine.Random.value;
    }

    private void UpdateValue()
    {
        _clickForce = Math.Round(Modifier.EnhancementClickForce * Modifier.PrestigeClickForce * Modifier.ADsBoost * Modifier.TimeMoneyBoost);       
        _criticalClickForce = Math.Round(_clickForce * _criticalModifier);

        _textClick = ConvertNumber.Convert(_clickForce);
        _criticalTextClick = ConvertNumber.Convert(_criticalClickForce);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _wallet ??= GetComponent<Wallet>();
        _message ??= GetComponent<MessageSystem>();
    }
#endif
}