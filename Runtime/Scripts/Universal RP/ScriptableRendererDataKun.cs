using PowerUtilities.UTJ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

        [SerializeField] LayerMask m_OpaqueLayerMask = -1;
        [SerializeField] LayerMask m_TransparentLayerMask = -1;
        [SerializeField] StencilStateData m_DefaultStencilState = new StencilStateData() { passOperation = StencilOp.Replace }; // This default state is compatible with deferred renderer.
        [SerializeField] bool m_ShadowTransparentReceive = true;
        [SerializeField] RenderingMode m_RenderingMode = RenderingMode.Forward;
        [SerializeField] DepthPrimingMode m_DepthPrimingMode = DepthPrimingMode.Disabled; // Default disabled because there are some outstanding issues with Text Mesh rendering.
        [SerializeField] CopyDepthMode m_CopyDepthMode = CopyDepthMode.AfterTransparents;
        [SerializeField] bool m_AccurateGbufferNormals = false;
        [SerializeField] IntermediateTextureMode m_IntermediateTextureMode = IntermediateTextureMode.Always;

        public ScriptableRendererDataKun() : this(null)
        {

        }

        public ScriptableRendererDataKun(ScriptableObject scriptableObject) : base(scriptableObject)
        {
            var data = scriptableObject as UniversalRendererData;
            if (!data)
                return;

            objJson = JsonUtility.ToJson(data);
            foreach(var feature in data.rendererFeatures)
            {
                featuresJson.Add((feature.GetType().FullName, JsonUtility.ToJson(feature)));
            }

            ReflectionTools.CopyFieldInfoValues(data, this, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
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
            // write all
            binaryWriter.Write((int)m_OpaqueLayerMask);
            binaryWriter.Write((int)m_TransparentLayerMask);
            binaryWriter.Write(JsonUtility.ToJson(m_DefaultStencilState));
            binaryWriter.Write(m_ShadowTransparentReceive);
            binaryWriter.Write((int)m_RenderingMode);
            binaryWriter.Write((int)m_DepthPrimingMode);
            binaryWriter.Write((int)m_CopyDepthMode);
            binaryWriter.Write(m_AccurateGbufferNormals);
            binaryWriter.Write((int)m_IntermediateTextureMode);
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
            // read all
            m_OpaqueLayerMask = binaryReader.ReadInt32();
            m_TransparentLayerMask = binaryReader.ReadInt32();
            m_DefaultStencilState = JsonUtility.FromJson<StencilStateData>(binaryReader.ReadString());
            m_ShadowTransparentReceive = binaryReader.ReadBoolean();
            m_RenderingMode = (RenderingMode)binaryReader.ReadInt32();
            m_DepthPrimingMode = (DepthPrimingMode)binaryReader.ReadInt32();
            m_CopyDepthMode = (CopyDepthMode)binaryReader.ReadInt32();
            m_AccurateGbufferNormals = binaryReader.ReadBoolean();
            m_IntermediateTextureMode = (IntermediateTextureMode)binaryReader.ReadInt32();
        }
        public void UpdateKunData(UniversalRendererData viewData)
        {
            ReflectionTools.CopyFieldInfoValues(viewData, this, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        public override bool WriteBack(UnityEngine.Object obj)
        {
            var data = obj as UniversalRendererData;
            if (!data)
                return false;

            ReflectionTools.CopyFieldInfoValues(this,data, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            data.SetDirty();

            return true;
        }


    }
}