using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using Utj.UnityChoseKun.Editor.Rendering;
using Utj.UnityChoseKun.Editor.Rendering.Universal;
using Utj.UnityChoseKun.Engine;


namespace  Utj.UnityChoseKun.Editor
{
    
    

    /// <summary>
    /// 用于显示Component的基类 
    /// Programed by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    public class ComponentView
    {
        // [NOTE] 如果添加了ComponentKun型的Class，请在此处添加
        // ComponentKunType,ComponentView的System.Type
        static Dictionary<ComponentKun.ComponentKunType, System.Type> componentViewTbls = new Dictionary<BehaviourKun.ComponentKunType, System.Type>{
            {ComponentKun.ComponentKunType.Transform,               typeof(TransformView)},
            {ComponentKun.ComponentKunType.Camera,                  typeof(CameraView)},
            {ComponentKun.ComponentKunType.Light,                   typeof(LightView)},

            {ComponentKun.ComponentKunType.SpriteRenderer,typeof(SpriteRendererView) },

            {ComponentKun.ComponentKunType.SkinnedMeshMeshRenderer, typeof(SkinnedMeshRendererView)},
            {ComponentKun.ComponentKunType.MeshRenderer,            typeof(MeshRendererView)},
            {ComponentKun.ComponentKunType.Renderer,                typeof(RendererView)},

            {ComponentKun.ComponentKunType.Rigidbody,               typeof(RigidbodyView)},

            {ComponentKun.ComponentKunType.MeshCollider,            typeof(MeshColliderView) },
            {ComponentKun.ComponentKunType.CapsuleCollider,         typeof(CapsuleColliderView) },
            {ComponentKun.ComponentKunType.Collider,                typeof(ColliderView) },

            {ComponentKun.ComponentKunType.Animator,                typeof(AnimatorView) },
            {ComponentKun.ComponentKunType.ParticleSystem,          typeof(ParticleSystemView) },

            {ComponentKun.ComponentKunType.Canvas,                  typeof(CanvasView) },

            // ===

            {ComponentKun.ComponentKunType.Volume,typeof(VolumeView) },
            {ComponentKun.ComponentKunType.UniversalAdditionalCameraData,   typeof(UniversalAdditionalCameraDataView) },
            {ComponentKun.ComponentKunType.UniversalAdditionalLightData,    typeof(UniversalAdditionalLightDataView)},
            {ComponentKun.ComponentKunType.MonoBehaviour,                   typeof(MonoBehaviourView)},
            {ComponentKun.ComponentKunType.Behaviour,                       typeof(BehaviourView)},
            {ComponentKun.ComponentKunType.Component,                       typeof(ComponentView)},

            {ComponentKun.ComponentKunType.MissingMono,            typeof(MissingMonoView) },

            {ComponentKun. ComponentKunType.TestMono,                 typeof(TestMonoView) },
        };
        
        static ComponentView()
        {
        }

        public static System.Type GetComponentViewSyetemType(BehaviourKun.ComponentKunType componentType)
        {
            System.Type type;

            if(componentViewTbls.TryGetValue(componentType, out type))
            {
                return type;
            }
            return typeof(ComponentView);            
        }

        private static class Styles
        {
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_Prefab Icon");
        }


        [SerializeField] ComponentKun m_componentKun;
        protected ComponentKun componentKun
        {
            get { if (m_componentKun == null) { m_componentKun = new ComponentKun(); } return m_componentKun; }
            set { m_componentKun = value; }
        }

        /// <summary>
        /// 显示在Class名旁边的图标
        /// </summary>
        protected Texture2D mComponentIcon;
        protected Texture2D componentIcon
        {
            get { return mComponentIcon;}
            set { mComponentIcon = value;}
        }
        
        /// <summary>
        /// Foldout的值
        /// </summary>
        [SerializeField] bool mFoldout;
        protected bool foldout
        {
            get { return mFoldout; }
            set { mFoldout = value; }
        }

        /// <summary>
        /// 设置ComponentKun
        /// </summary>
        /// <param name="componentKun">要设置的ComponentKun</param>
        public virtual void SetComponentKun(ComponentKun componentKun)
        {
            this.componentKun = componentKun;
        }


        /// <summary>
        /// 获取ComponentKun
        /// </summary>
        /// <returns>ComponentKun</returns>
        public virtual ComponentKun GetComponentKun()
        {
            return componentKun;
        }


        public ComponentView()
        {
            componentIcon = Styles.ComponentIcon;

            foldout = false;
        }


        /// <summary>
        /// 从OnGUI调用的处理
        /// </summary>
        public virtual bool OnGUI()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.BeginHorizontal();                        
            var iconContent = new GUIContent(mComponentIcon);
            foldout = EditorGUITools.Foldout(foldout, iconContent);
                         
            EditorGUI.BeginChangeCheck();
            var rect = EditorGUILayout.GetControlRect();
            var content = new GUIContent(componentKun.name);
            EditorGUI.LabelField(new Rect(rect.x - 8,rect.y,rect.width,rect.height),content);
            //EditorGUILayout.LabelField(content);

            if (EditorGUI.EndChangeCheck()){
                componentKun.dirty = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            return foldout;
        }
    }
}