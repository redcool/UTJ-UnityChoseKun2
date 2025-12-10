using PowerUtilities.UTJ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MonoBehaviourAutoKun()]
public class TestMonoAuto : MonoBehaviour
{
    public int id;
    public string log;

    string lastLog;
    void Update()
    {
        if(lastLog != log)
        {
            lastLog = log;
            Debug.Log($"log changed {log}");
        }
    }
}
