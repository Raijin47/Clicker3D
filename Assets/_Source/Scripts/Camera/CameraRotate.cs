using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Joystick _joystick;

    [SerializeField] private float _sensitivity;
    [SerializeField] private float _smooth;
    [SerializeField] private float _minAngle, _maxAngle;

    private float _dirX, _dirY;

    private void LateUpdate()
    {
        _dirY += _joystick.Direction.x * _sensitivity;
        _dirX -= _joystick.Direction.y * _sensitivity;

        _dirX = Mathf.Clamp(_dirX, _minAngle, _maxAngle);

        _camera.rotation = Quaternion.Lerp(_camera.rotation, Quaternion.Euler(_dirX, _dirY, 0), Time.deltaTime * _smooth);
    }
}