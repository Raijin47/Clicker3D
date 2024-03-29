using UnityEngine;
using System.Collections;

public class TransparentScreenshot : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private KeyCode key; // клавиша управления в игровом режиме
    [SerializeField] private int width = 1920;
    [SerializeField] private int height = 1080;
    [SerializeField] private bool isTransparent = true; // прозрачный фон или по умолчанию
    private byte[] bytes;

    string ScreenShotName()
    {
        return string.Format("{0}/screen_{1}x{2}_{3}.png",
            Application.dataPath,
            width, height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void Screenshot()
    {
        if (isTransparent) _camera.clearFlags = CameraClearFlags.Depth;
        RenderTexture rt = new RenderTexture(width, height, 24);
        _camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.ARGB32, false);
        _camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        _camera.targetTexture = null;
        RenderTexture.active = null;
#if UNITY_EDITOR
        DestroyImmediate(rt);
#else
		Destroy(rt);
#endif
        bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName();
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log("Создан скриншот: " + filename);
#if UNITY_EDITOR
        DestroyImmediate(screenShot);
#else
		Destroy(screenShot);
#endif
        bytes = new byte[0];
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(key))
        {
            Screenshot();
        }
    }
}