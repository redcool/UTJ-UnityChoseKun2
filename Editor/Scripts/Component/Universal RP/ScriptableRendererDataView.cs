using PowerUtilities.UTJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utj.UnityChoseKun.Editor;
using Utj.UnityChoseKun.Engine.Rendering.Universal;

namespace UTJ.UnityChoseKun
{
    public class ScriptableRendererDataView : BehaviourView
    {
        UniversalRendererData soObj;

        UniversalRendererDataEditor soObjEditor;

        public override bool OnGUI()
        {
            return base.OnGUI();
        }

        public void Draw(ScriptableRendererDataKun data)
        {
            if (!soObj)
            {
                soObj = ScriptableObject.CreateInstance<UniversalRendererData>();
                WriteBackRendererData(data, soObj);

                soObjEditor = (UniversalRendererDataEditor)Editor.CreateEditor(soObj);
            }
            soObjEditor.OnInspectorGUI();
        }

        public void WriteBackRendererData(ScriptableRendererDataKun data, ScriptableRendererData soObj)
        {
            // write data except features
            JsonUtility.FromJsonOverwrite(data.objJson, soObj);

            // write features
            soObj.rendererFeatures.Clear();
            foreach (var featureJson in data.featuresJson)
            {
                var feature = (ScriptableRendererFeature)ScriptableObject.CreateInstance(featureJson.typeFullName);
                JsonUtility.FromJsonOverwrite(featureJson.json, feature);
                soObj.rendererFeatures.Add(feature);
            }
        }
    }
}
