using UnityEngine;
using YG;

public class ResetSave : MonoBehaviour
{
    public void ResetDataButton()
    {
        YandexGame.ResetSaveProgress();
    }
}