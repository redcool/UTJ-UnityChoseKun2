using PowerUtilities.UTJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utj.UnityChoseKun.Editor;
using Utj.UnityChoseKun.Engine.Rendering.Universal;
using Object = UnityEngine.Object;

namespace UTJ.UnityChoseKun
{
    public class ScriptableRendererDataView : BehaviourView
    {
        UniversalRendererData soObj;

        UniversalRendererDataEditor soObjEditor;
        string lastHash = "";

        public override bool OnGUI()
        {
            return base.OnGUI();
        }

        public bool Draw(ScriptableRendererDataKun dataKun)
        {
            if (!soObj)
            {
                soObj = ScriptableObject.CreateInstance<UniversalRendererData>();
                SetupData(dataKun, soObj);

                soObjEditor = (UniversalRendererDataEditor)Editor.CreateEditor(soObj);
                lastHash = MD5Tools.GetMD5HashString(null,soObj);
            }
            soObjEditor.OnInspectorGUI();
            
            
            var newHash = MD5Tools.GetMD5HashString(null,soObj);
            var isChanged = (newHash != lastHash);
            if (isChanged)
            {
                WriteBack(soObj, dataKun);
                lastHash = newHash;
            }
            return isChanged;
        }

        public void SetupData(ScriptableRendererDataKun dataKun, ScriptableRendererData soObj)
        {
            JsonUtility.FromJsonOverwrite(dataKun.objJson, soObj);
            soObj.rendererFeatures.Clear();
        }

        public void WriteBack(UniversalRendererData soObj,ScriptableRendererDataKun dataKun)
        {
            dataKun.objJson = JsonUtility.ToJson(soObj);
            dataKun.UpdateKunData(soObj);
        }
    }
}
