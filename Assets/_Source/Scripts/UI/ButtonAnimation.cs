using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Sub-Classes
    [System.Serializable]
    public class UIButtonEvent : UnityEvent<PointerEventData.InputButton> { }
    #endregion

    public UIButtonEvent OnButtonPress;
	public UIButtonEvent OnButtonRelease;

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		if (OnButtonPress != null)
		{
			OnButtonPress.Invoke(eventData.button);
		}
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		if (OnButtonRelease != null)
		{
			OnButtonRelease.Invoke(eventData.button);
		}
	}
}