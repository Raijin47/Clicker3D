using System.Collections;
using UnityEngine;

public class PetBonus : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleBonus, _particleGetBonus;
    [SerializeField] private ParticleSystem _particleDiamond;
    [SerializeField] private Transform _transform;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _bonusCreationFrequency;
    [SerializeField] private float _chanceCreateBonus;
    [SerializeField] private float _minDiamondReward, _maxDiamondReward;

    private Coroutine _updateTimerCoroutine;
    private Coroutine _updateRayCoroutine;

    private Timer _timer;

    private void Start()
    {
        _timer = new Timer(_bonusCreationFrequency);
        StartTimer();
        GlobalEvent.OnRebith.AddListener(DestroyBonus);
    }

    private void StartTimer()
    {
        StopActiveCoroutine();
        _updateTimerCoroutine = StartCoroutine(UpdateTimer());
    }

    private void StartRay()
    {
        StopActiveCoroutine();
        _updateRayCoroutine = StartCoroutine(UpdateRay());
    }

    private void StopActiveCoroutine()
    {
        if (_updateRayCoroutine != null)
        {
            StopCoroutine(_updateRayCoroutine);
            _updateRayCoroutine = null;
        }
        if (_updateTimerCoroutine != null)
        {
            StopCoroutine(_updateTimerCoroutine);
            _updateTimerCoroutine = null;
        }
    }

    private IEnumerator UpdateTimer()
    {
        while(true)
        {
            _timer.Update();
            if(_timer.IsCompleted)
            {
                if (Locator.Instance.Pets.IsPetActive() && CanCreate())
                {
                    ActivateBonus(Locator.Instance.Pets.GetPosition());
                    _timer.RestartTimer();
                }
                else
                {
                    _timer.RestartTimer();
                }
            }
            yield return null;
        }
    }

    private bool CanCreate()
    {
        return _chanceCreateBonus > Random.value;
    }

    private IEnumerator UpdateRay()
    {
        while(true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray))
                {
                    GetBonus();
                }
            }
            yield return null;
        }
    }

    private void GetBonus()
    {
        double reward = DiamondReward();
        Locator.Instance.Wallet.Diamonds += reward;
        Locator.Instance.RewardPanel.OpenPanel(1, reward);
        _particleDiamond.Play();

        SFXController.OnGetDiamonds?.Invoke();

        DestroyBonus();
        _particleGetBonus.Play();
    }

    private void DestroyBonus()
    {
        _collider.enabled = false;
        _particleBonus.Stop();
        StartTimer();
    }

    private double DiamondReward()
    {
        return (int)Random.Range(_minDiamondReward, _maxDiamondReward);
    }

    private void ActivateBonus(Vector3 position)
    {
        _collider.enabled = true;
        _transform.position = position;
        _particleBonus.Play();
        StartRay();
    }
}