using UnityEngine;

public class ButtonAnimationCore
{
    private static readonly Vector2 _pressedSize = new(0.93f, 0.93f);
    public static void Pressed(RectTransform rectTransform)
    {
        rectTransform.localScale = _pressedSize;
    }

    public static void Release(RectTransform rectTransform)
    {
        rectTransform.localScale = Vector2.one;
    }
}