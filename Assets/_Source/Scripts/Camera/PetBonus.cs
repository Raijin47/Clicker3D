using System.Collections;
using UnityEngine;

public class PetBonus : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleBonus, _particleGetBonus;
    [SerializeField] private ParticleSystem _particleGold, _particleDiamond;
    [SerializeField] private Transform _transform;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _bonusCreationFrequency;
    [SerializeField] private float _chanceCreateBonus;
    [SerializeField] private float _chanceCreateDiamond;
    [SerializeField] private double _modifierMoney;
    [SerializeField] private float _minDiamondReward, _maxDiamondReward;

    private Coroutine _updateTimerCoroutine;
    private Coroutine _updateRayCoroutine;

    private Timer _timer;

    private void Start()
    {
        _timer = new Timer(_bonusCreationFrequency);
        StartTimer();
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
                if (Locator.Instance.PetsManager.IsPetActive() && CanCreate())
                {
                    ActivateBonus(Locator.Instance.PetsManager.GetPosition());
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
        _collider.enabled = false;
        _particleBonus.Stop();
        _particleGetBonus.Play();

        if(_chanceCreateDiamond > Random.value)
        {
            Locator.Instance.Wallet.Diamonds += DiamondReward();
            _particleDiamond.Play();
        }
        else
        {
            Locator.Instance.Wallet.Money += MoneyReward();
            _particleGold.Play();
        }


        StartTimer();
    }

    private double DiamondReward()
    {
        return (int)Random.Range(_minDiamondReward, _maxDiamondReward);
    }

    private double MoneyReward()
    {
        return (Locator.Instance.Click.ClickIncome + Locator.Instance.JobsManager.CurrentIncome) * _modifierMoney;
    }

    private void ActivateBonus(Vector3 position)
    {
        _collider.enabled = true;
        _transform.position = position;
        _particleBonus.Play();
        StartRay();
    }
}