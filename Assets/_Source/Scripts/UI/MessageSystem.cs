using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private MessageText[] _normalText, _criticalText;
    private int _normalNumber, _criticalNumber;

    public void NormalClick(string text)
    {
        _normalNumber++;
        if(_normalNumber > 9) _normalNumber = 0;

        _normalText[_normalNumber].Active(text);
    }
    public void CriticalClick(string text)
    {
        _criticalNumber++;
        if (_criticalNumber > 9) _criticalNumber = 0;

        _criticalText[_criticalNumber].Active(text);
    }
}