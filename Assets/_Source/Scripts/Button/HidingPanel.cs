using UnityEngine;
using UnityEngine.UI;
using YG;

public class HidingPanel : MonoBehaviour
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private Sprite _hideSprite;
    [SerializeField] private Sprite _showSprite;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector2 _hidingPosition;
    [SerializeField] private Vector2 _showPosition;
    [SerializeField] private Button _button;

    public bool IsShow
    {
        get => YandexGame.savesData.IsShowPanel;
        set
        {
            YandexGame.savesData.IsShowPanel = value;
            _imageIcon.sprite = value ? _hideSprite : _showSprite;
            _rectTransform.anchoredPosition = value ? _showPosition : _hidingPosition;
            SFXController.OnOpenPanel?.Invoke(value);
        }
    }

    private void Start() => _button.onClick.AddListener(() => IsShow = !IsShow);

    public void Init() => IsShow = IsShow;
}