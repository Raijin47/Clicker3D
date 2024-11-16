using Assets.SimpleLocalization;
using UnityEngine;
using YG;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private GameObject _panelSelect;
    [SerializeField] private TutorialManager _tutorialManager;

    public void Init()
    {
        if(!YandexGame.savesData.IsInitOfflineTimer)
        {
            _panelSelect.SetActive(true);
        }
        else
        {
            _panelSelect.SetActive(false);
            InitRead();
        }
    }

    public void InitRead()
    {
        LocalizationManager.Read();
        _tutorialManager.Init();
    }

    public void SetLanguage(string lang)
    {
        LocalizationManager.Language = lang;
        YandexGame.savesData.language = lang;
        YandexGame.SaveProgress();
    }
}