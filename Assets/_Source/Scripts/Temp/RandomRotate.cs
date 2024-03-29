using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public bool Rotate;
    public Transform[] _object;
    private void OnValidate()
    {
        if(Rotate)
        {
            for(int i = 0; i < _object.Length; i++)
            {
                _object[i].localRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
            }


            Rotate = false;
        }
    }
}
