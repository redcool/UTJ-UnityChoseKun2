Use Attributes to mark class Serialized/Deserialized
    MonoBehaviourKunAttribute
    
1 Write a MonoBehaviour class

```:TestMono.cs
using PowerUtilities.UTJ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utj.UnityChoseKun.Engine;

[MonoBehaviourKun(kunType = typeof(TestMonoKun))]
public class TestMono : MonoBehaviour
{
    public string path = "";
    string lastPath = "";
    
    void Update()
    {
        if (lastPath != path)
        {
            lastPath = path;
            Debug.Log("path has changed");
        }
    }
}
```
add class attribute "MonoBehaviourKun"

2 Write MonoBehaviourKun class

```:TestMonoKun.cs
using System.IO;
using UnityEngine;
using Utj.UnityChoseKun.Engine;


public class TestMonoKun : MonoBehaviourKun
{
    public string path = "";
    // ctor 0
    public TestMonoKun() : this(null)
    {
        
    }
    // ctor 1
    public TestMonoKun(TestMono tm) : base(tm)
    {
        if (tm)
            path = tm.path;
    }

    public override void Serialize(BinaryWriter binaryWriter)
    {
        base.Serialize(binaryWriter);
        binaryWriter.Write(path);
    }

    public override void Deserialize(BinaryReader binaryReader)
    {
        base.Deserialize(binaryReader);
        path = binaryReader.ReadString();
    }

    public override bool WriteBack(Component component)
    {
        var tm = component as TestMono;
        tm.path = path;
        return true;
    }
}

```

3 Write MonoBehaviourView class

```:TestMonoView.cs
using PowerUtilities.UTJ;
using UnityEditor;
using Utj.UnityChoseKun.Editor;
using Utj.UnityChoseKun.Engine;

[MonoBehaviourView(typeof(TestMonoKun))]
public class TestMonoView : MonoBehaviourView
{
    public override bool OnGUI()
    {
        var testMonoKun = behaviourKun as TestMonoKun;
        base.OnGUI();
        testMonoKun.path = EditorGUILayout.TextField(testMonoKun.path);
        return true;
    }
}

```

All done