using UnityEngine;
using UnityEngine.UI;

public class HidingPanel : MonoBehaviour
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private Sprite _hideSprite;
    [SerializeField] private Sprite _showSprite;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector2 _hidingPosition;
    [SerializeField] private Vector2 _showPosition;

    private bool _isShow;

    private void Start()
    {
        _isShow = true;
        Execute(_isShow);
    }
    public void ActionButton()
    {
        _isShow = !_isShow;
        Execute(_isShow);
    }

    private void Execute(bool isShow)
    {
        if (isShow)
        {
            _imageIcon.sprite = _hideSprite;
            _rectTransform.anchoredPosition = _showPosition;
        }
        else
        {
            _imageIcon.sprite = _showSprite;
            _rectTransform.anchoredPosition = _hidingPosition;
        }
    }
}