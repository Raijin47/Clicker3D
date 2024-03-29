using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _safemodePanel;

    public void Safemode(bool isOn)
    {
        _safemodePanel.SetActive(isOn);
        _camera.cullingMask = isOn ? 0 : -1;
    }
}