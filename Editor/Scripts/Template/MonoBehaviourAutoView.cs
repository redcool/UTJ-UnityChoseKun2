using Newtonsoft.Json;
using PowerUtilities.UTJ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Utj.UnityChoseKun.Editor;

public class MonoBehaviourAutoView : MonoBehaviourView
{

    public override bool OnGUI()
    {
        var isFold = base.OnGUI();
        if (componentKun is MonoBehaviourAutoKun autoKun)
        {
            EditorGUI.BeginChangeCheck();
            autoKun.json = EditorGUILayout.TextArea(autoKun.json);
            if (EditorGUI.EndChangeCheck())
            {
                autoKun.dirty = true;
            }
        }
        return isFold;
    }

}
