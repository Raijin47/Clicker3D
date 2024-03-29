using TMPro;
using UnityEngine;

public class TestPosition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _minXText, _maxXText, _minYText, _maxYText, _minZText, _maxZText;
    [SerializeField] private TextMeshProUGUI _rminXText, _rmaxXText, _rminYText, _rmaxYText, _rminZText, _rmaxZText;
    [SerializeField] private Transform _target;
    private float _minX, _maxX, _minY, _maxY, _minZ, _maxZ;

    void Update()
    {
        if(_minX > _target.position.x)
        {
            _minX = _target.position.x;
            _minXText.text = _minX.ToString();
        }
        if(_maxX < _target.position.x)
        {
            _maxX = _target.position.x;
            _maxXText.text = _maxX.ToString();
        }

        if (_minY > _target.position.y)
        {
            _minY = _target.position.y;
            _minYText.text = _minY.ToString();
        }
        if (_maxY < _target.position.y)
        {
            _maxY = _target.position.y;
            _maxYText.text = _maxY.ToString();
        }

        if (_minZ > _target.position.z)
        {
            _minZ = _target.position.z;
            _minZText.text = _minZ.ToString();
        }
        if (_maxZ < _target.position.z)
        {
            _maxZ = _target.position.z;
            _maxZText.text = _maxZ.ToString();
        }

        if(Input.GetMouseButtonDown(0))
        {
            _rminXText.text = _minX.ToString();
            _rmaxXText.text = _maxX.ToString();
            _rminYText.text = _minY.ToString();
            _rmaxYText.text = _maxY.ToString();
            _rminZText.text = _minZ.ToString();
            _rmaxZText.text = _maxZ.ToString();
        }
    }
}
