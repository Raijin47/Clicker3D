using UnityEngine;
using UnityEngine.UI;

public class SelectionBase : ISelection
{
    private CanvasGroup _canvasGroup;
    private Image _image;
    private RectTransform _rectTransform;

    public SelectionBase(CanvasGroup canvas, Image image, RectTransform rectTransform)
    {
        _canvasGroup = canvas;
        _image = image;
        _rectTransform = rectTransform;
    }
    public virtual void Enter()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        _rectTransform.localScale = new Vector2(1.05f,1.05f);
        _image.color = new Color(1f, 1f, 1f);     
    }

    public virtual void Exit()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _rectTransform.localScale = Vector2.one;
        _image.color = new Color(0f, 0.47f, 0.59f);
    }
}