using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utj.UnityChoseKun.Engine;

public class MonoBehaviourAutoKun : MonoBehaviourKun
{
    // save component
    public Type componentType;

    public string json;

    public MonoBehaviourAutoKun() : base(null) { }
    public MonoBehaviourAutoKun(Component component) {
        if (!component)
            return;
        componentType = component.GetType();
        json = JsonUtility.ToJson(component,true);
    }

    public override void Deserialize(BinaryReader binaryReader)
    {
        base.Deserialize(binaryReader);
        json = binaryReader.ReadString();
    }

    public override void Serialize(BinaryWriter binaryWriter)
    {
        base.Serialize(binaryWriter);

        binaryWriter.Write(json);
    }

    public override bool WriteBack(Component component)
    {
        var isDirty = base.WriteBack(component);
        if (isDirty)
        {
            JsonUtility.FromJsonOverwrite(json,component);
        }
        return isDirty;
    }
}
