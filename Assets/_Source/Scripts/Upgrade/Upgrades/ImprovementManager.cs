using System.Linq;
using UnityEngine;

public class ImprovementManager : MonoBehaviour
{
    [SerializeField] private ImprovementBase[] _sorting;

    [SerializeField] private ImprovementPet[] _improvedPets;
    [SerializeField] private ImprovementJob[] _improvedJobs;
    [SerializeField] private ImprovementClick _improvedClick;
    [SerializeField] private ImprovementIsland _improvedIsland;
    [SerializeField] private GameObject _notImprovedText;

    [SerializeField] private Color[] _colors;

    public Color[] Color => _colors;
    public ImprovementPet[] Pets => _improvedPets;
    public ImprovementJob[] Jobs => _improvedJobs;
    public ImprovementClick Click => _improvedClick;
    public ImprovementIsland Island => _improvedIsland;

    public void Init()
    {
        for (int i = 0; i < _improvedJobs.Length; i++) _improvedJobs[i].Init();
        for (int i = 0; i < _improvedPets.Length; i++) _improvedPets[i].Init();
        _improvedClick.Init();
        _improvedIsland.Init();
    }

    public void OnReset()
    {
        for (int i = 0; i < _improvedJobs.Length; i++) _improvedJobs[i].Deactivate();
        for (int i = 0; i < _improvedPets.Length; i++) _improvedPets[i].Deactivate();
        _improvedClick.Deactivate();
        _improvedIsland.Deactivate();
    }



    public void SortingImproved()
    {    
        var sorting = _sorting.OrderBy(i => i.Price).ToArray();

        for(int i = 0; i < sorting.Length; i++)
        {
            sorting[i].transform.SetAsLastSibling();
        }

        bool isNotImproved = false;

        for(int i = 0; i < _sorting.Length; i++)
        {
            isNotImproved = !_sorting[i].gameObject.activeSelf;
            if(!isNotImproved) break;
        }

        _notImprovedText.SetActive(isNotImproved);
    }
}