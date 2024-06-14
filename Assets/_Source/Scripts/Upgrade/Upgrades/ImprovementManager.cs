using UnityEngine;

public class ImprovementManager : MonoBehaviour
{
    [SerializeField] private ImprovementPet[] _improvedPets;
    [SerializeField] private ImprovementJob[] _improvedJobs;
    [SerializeField] private ImprovementBase[] _upgradesDays;

    [SerializeField] private Color[] _colors;
    public Color[] Color => _colors;
    public ImprovementPet[] ImprovedPets => _improvedPets;
    public ImprovementJob[] ImprovedJobs => _improvedJobs;

    public void Init()
    {
        GlobalEvent.OnRebith.AddListener(OnReset);
        for (int i = 0; i < _improvedJobs.Length; i++) _improvedJobs[i].Init();
        for (int i = 0; i < _improvedPets.Length; i++) _improvedPets[i].Init();
    }

    private void OnReset()
    {
        for (int i = 0; i < _improvedJobs.Length; i++) _improvedJobs[i].Deactivate();
        for (int i = 0; i < _improvedPets.Length; i++) _improvedPets[i].Deactivate();
    }
}