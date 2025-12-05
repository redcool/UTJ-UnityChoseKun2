using PowerUtilities.UTJ;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utj.UnityChoseKun.Editor;
using Utj.UnityChoseKun.Engine;
using UTJ.UnityChoseKun;

[MonoBehaviourView(typeof(TestMonoKun))]
public class TestMonoView : MonoBehaviourView
{
    public TestMonoKun testMonoKun
    {
        get { return behaviourKun as TestMonoKun; }
        set { behaviourKun = value; }
    }

    public override void SetComponentKun(ComponentKun componentKun)
    {
        testMonoKun = (TestMonoKun)componentKun;
    }
    public override ComponentKun GetComponentKun()
    {
        return testMonoKun;
    }

    public override bool OnGUI()
    {
        base.OnGUI();
        testMonoKun.path = EditorGUILayout.TextField(testMonoKun.path);
        return true;
    }
}
