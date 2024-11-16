using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Sub-Classes
    [System.Serializable]
    public class UIButtonEvent : UnityEvent<PointerEventData.InputButton> { }
	#endregion

	[SerializeField] private RectTransform _rectTransform;

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		ButtonAnimationCore.Pressed(_rectTransform);
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		ButtonAnimationCore.Release(_rectTransform);
	}
}