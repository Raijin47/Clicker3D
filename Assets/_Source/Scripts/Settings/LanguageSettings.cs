using Assets.SimpleLocalization;
using UnityEngine;
using YG;

public class LanguageSettings : MonoBehaviour
{
    public void Init()
    {
        if (YandexGame.savesData.isFirstSession)
        {
            LocalizationManager.Language = YandexGame.lang;
        }
        else
        {
            LocalizationManager.Language = YandexGame.savesData.language;
        }
        LocalizationManager.Read();
    }

    public void SetLanguage(string lang)
    {
        LocalizationManager.Language = lang;
        YandexGame.savesData.language = lang;
    }
}