using PowerUtilities.UTJ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utj.UnityChoseKun.Engine;

[MonoBehaviourKun(typeof(TestMonoKun))]
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
