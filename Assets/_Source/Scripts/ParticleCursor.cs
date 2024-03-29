using System.Collections;
using UnityEngine;

public class ParticleCursor : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private ParticleSystem _particle;
    private Coroutine _updatePositionCoroutine;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            _particle.Play();
            if(_updatePositionCoroutine != null)
            {
                StopCoroutine(_updatePositionCoroutine);
                _updatePositionCoroutine = null;
            }
            _updatePositionCoroutine = StartCoroutine(UpdatePositionProcess());
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            _particle.Stop();
            if (_updatePositionCoroutine != null)
            {
                StopCoroutine(_updatePositionCoroutine);
                _updatePositionCoroutine = null;
            }
        }
    }

    private IEnumerator UpdatePositionProcess()
    {
        while(true)
        {
            _transform.position = Input.mousePosition;
            yield return null;
        }
    }
}