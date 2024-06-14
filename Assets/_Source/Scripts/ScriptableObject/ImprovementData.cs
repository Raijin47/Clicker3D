using UnityEngine;

[CreateAssetMenu(fileName = "Improvement", menuName = "ScriptableObjects/ImprovementData", order = 51)]
public class ImprovementData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private double[] _prices;    
    [SerializeField] private double[] _increasesValue;
    [SerializeField] private double[] _increasesPercent;

    public string Name => _name;
    public double[] Price => _prices;
    public double[] IncreasesValue => _increasesValue;   
    public double[] IncreasesPercent => _increasesPercent;
}