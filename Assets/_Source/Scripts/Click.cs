using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Camera _camera;
    [SerializeField] private MessageSystem _message;
    [SerializeField] private double _criticalModifier;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private RectTransform _particleTransform;
    [SerializeField] private EnhancementForceClick _clickForce;

    public void Init()
    {

    }

    public void ClickButton()
    {
        _particleTransform.position = Input.mousePosition;
        _particle.Play();

        _message.NormalClick(_clickForce.ClickForceText);
        _wallet.Money += _clickForce.ClickForce;

        SFXController.OnClickButton?.Invoke();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _wallet ??= GetComponent<Wallet>();
        _message ??= GetComponent<MessageSystem>();
    }
#endif
}