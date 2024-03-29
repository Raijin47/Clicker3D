using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] private float _lookSpeed;
    [SerializeField] private Transform _target;

    private Quaternion _rotGoal;
    private Vector3 _direction;

    private void LateUpdate()
    {
        _direction = (_target.position - transform.position).normalized;
        _rotGoal = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotGoal, _lookSpeed);
    }
}