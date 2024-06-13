using TMPro;
using UnityEngine;

public class CountUpgradeButton : MonoBehaviour
{
    public enum CountState
    {
        x1,
        x10,
        x100,
    }
    private readonly string[] stateUpgradeText = new string[3]
    {
        "x1", "x10", "x100",
    };

    [SerializeField] private TextMeshProUGUI _countUpgradeText;
    public CountState CurrentState { get; private set; }

    public void SwitchState()
    {
        switch(CurrentState)
        {
            case CountState.x1:
                CurrentState = CountState.x10;;
                break;
            case CountState.x10:
                CurrentState = CountState.x100;
                break;
            case CountState.x100:
                CurrentState = CountState.x1;
                break;
        }

        _countUpgradeText.text = stateUpgradeText[(int)CurrentState];
        GlobalEvent.SendChangeCountUpgrade();
    }
}