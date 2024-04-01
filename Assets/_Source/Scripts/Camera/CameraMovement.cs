using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _target ;

    [SerializeField] private float _offset;
    [SerializeField] private float _smooth;

    private void LateUpdate()
    {
        Vector3 cameraTarget = new(_target.position.x, _offset, _target.position.z);
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, cameraTarget, _smooth * Time.deltaTime);
    }
}