using UnityEngine;

public class LookAtUnity : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _transforms;
    public bool IsLook;
    private void OnValidate()
    {
        if(IsLook)
        {
            for (int i = 0; i < _transforms.Length; i++)
            {
                _transforms[i].LookAt(new Vector3(0f, _target.position.y, 0f));
            }
            IsLook = false;
        }
    }
}