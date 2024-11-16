using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private MessageText[] _normalText;
    private int _normalNumber;

    public void NormalClick(string text)
    {
        _normalNumber++;
        if(_normalNumber > 9) _normalNumber = 0;

        _normalText[_normalNumber].Active(text);
    }
}