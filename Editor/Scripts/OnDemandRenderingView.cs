using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class OnDemandRenderingView
    {
        // 成员变量的定义

        [SerializeField] OnDemandRenderingKun mOnDemandRenderingKun;


        // 属性的定义

        OnDemandRenderingKun onDemandRenderingKun
        {
            get {
                if(mOnDemandRenderingKun == null)
                {
                    mOnDemandRenderingKun = new OnDemandRenderingKun();
                }
                return mOnDemandRenderingKun;
            }

            set
            {
                mOnDemandRenderingKun = value;
            }
        }

        Vector2 scrollPos
        {
            get;
            set;
        }


        static OnDemandRenderingView mInstance;
        public static OnDemandRenderingView instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new OnDemandRenderingView();
                }
                return mInstance;
            }
        }


        // 成员函数的定义

        /// <summary>
        /// 绘制
        /// </summary>
        public void OnGUI()
        {
#if UNITY_2019_3_OR_NEWER
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("effectiveRenderFrameRate", "根据当前设置预期的绘制[FPS]"));
            EditorGUILayout.LabelField(onDemandRenderingKun.effectiveRenderFrameRate.ToString() + "[FPS]");
            //EditorGUILayout.Toggle(new GUIContent("willCurrentFrameRender","当前帧是否是发生绘制的帧"), onDemandRenderingKun.willCurrentFrameRender);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            onDemandRenderingKun.renderFrameInterval = EditorGUILayout.IntSlider(new GUIContent("renderFrameInterval","进行绘制的帧间隔"), onDemandRenderingKun.renderFrameInterval, 1, 100);
            if (EditorGUI.EndChangeCheck())
            {
                onDemandRenderingKun.isDirty = true;
                UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPush, onDemandRenderingKun);
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();
            //if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPull, null);
            }
            //if (GUILayout.Button("Push"))
            {
               // UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPush, onDemandRenderingKun);
            }
            EditorGUILayout.EndHorizontal();
#else
            EditorGUILayout.LabelField("Not Support OnDemandRendering");
#endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEvent(BinaryReader binaryReader)
        {
            onDemandRenderingKun.Deserialize(binaryReader);

        }
    }
}
