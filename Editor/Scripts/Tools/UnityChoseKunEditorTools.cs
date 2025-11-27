#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Utj.UnityChoseKun.Editor
{
    public static class UnityChoseKunEditorTools
    {
        public static void DrawScriptType(Type scriptType)
        {
            GUILayout.BeginHorizontal("helpbox");
            GUILayout.Label("Target Type:", GUILayout.Width(100), GUILayout.Height(16));
            EditorGUILayout.SelectableLabel(scriptType.FullName, GUILayout.Height(16));
            GUILayout.EndHorizontal();
        }


    }
}
#endif