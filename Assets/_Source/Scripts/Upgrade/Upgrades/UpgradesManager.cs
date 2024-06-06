using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private UpgradesBase[] _upgradesBases;

    public void Init()
    {
        GlobalEvent.OnRebith.AddListener(OnReset);
        for (int i = 0; i < _upgradesBases.Length; i++) _upgradesBases[i].Init();
    }

    private void OnReset()
    {
        for (int i = 0; i < _upgradesBases.Length; i++) _upgradesBases[i].Deactivate();
    }
}