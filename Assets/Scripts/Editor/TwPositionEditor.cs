using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TwPosition))]
public class TwPositionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TwPosition twPosition = (TwPosition)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Set Pos A"))
        {
            twPosition.SetPosA();
        }

        if (GUILayout.Button("Set Pos B"))
        {
            twPosition.SetPosB();
        }

        GUILayout.EndHorizontal();
    }
}
