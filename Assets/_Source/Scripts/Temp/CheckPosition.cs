using TMPro;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _minXText;
    [SerializeField] private TextMeshProUGUI _maxXText;
    [SerializeField] private TextMeshProUGUI _minZText;
    [SerializeField] private TextMeshProUGUI _maxZText;

    [SerializeField] private Transform _target;
    private float _minX;
    private float _maxX;
    private float _minZ;
    private float _maxZ;


    void Update()
    {
        if(_target.position.x > _maxX)
        {
            _maxX = _target.position.x;
            _maxXText.text = "MAX X: " + _maxX;
        }
        if(_target.position.x < _minX)
        {
            _minX = _target.position.x;
            _minXText.text = "MIN X: " + _minX;
        }

        if (_target.position.z > _maxZ)
        {
            _maxZ = _target.position.z;
            _maxZText.text = "MAX Z: " + _maxZ;
        }
        if (_target.position.z < _minX)
        {
            _minZ = _target.position.z;
            _minZText.text = "MIN Z: " + _minZ;
        }
    }
}
