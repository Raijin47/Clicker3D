using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

[Serializable]
public class SkinColor
{
    [SerializeField] private GameObject _lockButton;
    [SerializeField] private Image _image;
    [SerializeField] private int _id;

    public Color Color => _image.color;

    public void Init() => _lockButton.SetActive(!YandexGame.savesData.ColorPurchased[_id]);
    public void Unlock()
    {
        YandexGame.savesData.ColorPurchased[_id] = true;
        _lockButton.SetActive(false);
    }
}