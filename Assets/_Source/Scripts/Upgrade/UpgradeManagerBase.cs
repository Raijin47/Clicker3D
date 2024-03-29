using UnityEngine;

public class UpgradeManagerBase : MonoBehaviour
{
    [SerializeField] private UpgradeBase[] _upgradeBases;

    public virtual void Init()
    {
        for (int i = 0; i < _upgradeBases.Length; i++)
        {
            _upgradeBases[i].Init();
        }
    }
}