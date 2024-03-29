#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TransparentScreenshot))]

public class TransparentScreenshotEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TransparentScreenshot t = (TransparentScreenshot)target;
        GUILayout.Label("Создать скриншот в инспекторе:", EditorStyles.boldLabel);
        if (GUILayout.Button("Take Screenshot"))
        {
            t.Screenshot();
        }
    }
}
#endif