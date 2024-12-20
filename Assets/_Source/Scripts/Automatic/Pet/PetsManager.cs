using UnityEngine;
using YG;

public class PetsManager : AutoBaseManager
{
    [SerializeField] private GameObject[] _pets;
    public GameObject[] Pets => _pets;

    public bool IsPetActive()
    {
        return _id != 0; 
    }

    public Vector3 GetPosition()
    {
        return _pets[Random.Range(0, _id)].transform.position;
    }

    protected override void Activate(int i)
    {
        _autoBases[i].Activate(YandexGame.savesData.PetLevel[i]);
    }

    protected override void AddAdditionalListener()
    {
        GlobalEvent.OnIncreasePetIncome.AddListener(RecalculateAutoBase);
    }

    protected override void GetIncome()
    {
        Locator.Instance.Health.CurrentHealth += _income;
    }

    protected override void SaveAuto()
    {
        YandexGame.savesData.CurrentPet = _id;
    }
}