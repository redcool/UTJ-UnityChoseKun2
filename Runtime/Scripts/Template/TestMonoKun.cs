using PowerUtilities.UTJ;
using System;
using System.IO;
using UnityEngine;
using Utj.UnityChoseKun.Engine;


public class TestMonoKun : MonoBehaviourKun
{
    public string path = "";

    public TestMonoKun() : this(null)
    {
        
    }
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
