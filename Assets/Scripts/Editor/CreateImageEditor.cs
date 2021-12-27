using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CreateImageEditor : EditorWindow
{

    [MenuItem("SafeTools/GenerateImage")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyCreateImageEditorWindow window = (MyCreateImageEditorWindow)EditorWindow.GetWindow(typeof(MyCreateImageEditorWindow));
        window.Show();
    }

}

public class MyCreateImageEditorWindow : EditorWindow
{

    public GameObject source;

    void OnGUI()
    {
        GUILayout.BeginVertical();
        source = EditorGUILayout.ObjectField(source, typeof(Object), true) as GameObject;
        GUILayout.EndVertical();
        if (GUILayout.Button("Gen"))
        {
            if (source != null && Selection.objects.Length > 0)
            {
                foreach (Object o in Selection.objects)
                {
                    string path = AssetDatabase.GetAssetPath(o);
                    SpriteRenderer image = new GameObject().AddComponent<SpriteRenderer>();
                    image.transform.parent = source.transform;
                    image.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                    image.gameObject.name = o.name;
                }
            }
        }
    }
}
