using PowerUtilities.UTJ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Katsumasa.Kimura

namespace Utj.UnityChoseKun.Engine.Rendering.Universal
{
    [Serializable]
    /// <summary>
    /// Class <c>ScriptableRendererDataKun</c> <c>ScriptableRendererData</c>
    /// </summary>
    public class ScriptableRendererDataKun : ScriptableObjectKun
    {
        public bool isFolded;
        public string objJson = "";
        public List<(string typeFullName,string json)> featuresJson = new();

        public LayerMask opaqueLayers;
        public ScriptableRendererDataKun() : this(null)
        {

        }

        public ScriptableRendererDataKun(ScriptableObject scriptableObject) : base(scriptableObject)
        {
            var data = scriptableObject as UniversalRendererData;
            Debug.Log(scriptableObject);
            if (!data)
                return;
            opaqueLayers = data.opaqueLayerMask;

            objJson = JsonUtility.ToJson(data);
            foreach(var feature in data.rendererFeatures)
            {
                featuresJson.Add((feature.GetType().FullName, JsonUtility.ToJson(feature)));
            }
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(objJson);
            binaryWriter.Write(featuresJson.Count);
            foreach (var featureJson in featuresJson)
            {
                binaryWriter.Write(featureJson.typeFullName);
                binaryWriter.Write(featureJson.json);
            }
            binaryWriter.Write((int)opaqueLayers);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            objJson = binaryReader.ReadString();
            featuresJson.Clear();
            var count = binaryReader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var featureFullName = binaryReader.ReadString();
                var featureJson = binaryReader.ReadString();
                featuresJson.Add((featureFullName, featureJson));
            }
            opaqueLayers = binaryReader.ReadInt32();
        }
        public override bool WriteBack(UnityEngine.Object obj)
        {
            Debug.Log($"=========================={obj}");
            if (dirty)
            {
                var data = obj as UniversalRendererData;
                if (!data)
                    return false;
                data.opaqueLayerMask = opaqueLayers;
                //var features = data.rendererFeatures;

                //JsonUtility.FromJsonOverwrite(objJson, data);
                //data.rendererFeatures.Clear();
                //foreach(var feature in features)
                //{
                //    data.rendererFeatures.Add(feature);
                //}
                return true;
            }
            return false;
        }


    }
}