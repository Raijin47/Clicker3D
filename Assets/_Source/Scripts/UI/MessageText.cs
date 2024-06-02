using System.Collections;
using TMPro;
using UnityEngine;

public class MessageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private RectTransform _textTransform;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _moveSpeed;

    private Coroutine _updatePositionCoroutine;
    private Coroutine _updateWaitToCleanCoroutine;
    private Timer _timer;
    private void Awake()
    {
        _timer = new Timer(_lifeTime);
    }
    public void Active(string text)
    {
        gameObject.SetActive(true);
        _textTransform.position = Input.mousePosition;
        _messageText.text = text;
    }

    private void OnEnable()
    {
        _timer.RestartTimer();
        if(_updatePositionCoroutine != null)
        {
            StopCoroutine(_updatePositionCoroutine);
            _updatePositionCoroutine = null;
        }
        _updatePositionCoroutine = StartCoroutine(UpdatePosition());
    }

    private void OnDisable()
    {
        if (_updatePositionCoroutine != null)
        {
            StopCoroutine(_updatePositionCoroutine);
            _updatePositionCoroutine = null;
        }
    }
    private IEnumerator UpdatePosition()
    {
        while(!_timer.IsCompleted)
        {
            _textTransform.position += _moveSpeed * Time.deltaTime * Vector3.up;
            _timer.Update();
            yield return null;
        }

        yield return WaitToClean();
        gameObject.SetActive(false);
    }

    private IEnumerator WaitToClean()
    {
        _timer.RestartTimer();
        while (!_timer.IsCompleted)
        {
            _timer.Update();
            yield return null;
        }
    }
}