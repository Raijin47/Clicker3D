using Assets.SimpleLocalization;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [SerializeField] private GameObject _helperPanel;
    [SerializeField] private LocalizedText _text;
    [SerializeField] private LocalizedText _textTitle;

    private readonly string _help = "Help";
    private readonly string _helpTitle = "HelpTitle";

    public void ShowHelp(int id)
    {
        _helperPanel.SetActive(true);
        _text.SetKey(_help + id);
        _textTitle.SetKey(_helpTitle + id);
    }
}