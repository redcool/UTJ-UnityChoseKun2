using UnityEngine;
using UnityEditor;
//using UnityEditor.UIElements;

namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        public class RigidbodyView : ComponentView
        {
            private static class Styles
            {
                public static GUIContent Rigidbody = new GUIContent("Rigidbody", (Texture2D)EditorGUIUtility.Load("d_Rigidbody Icon"));
                public static GUIContent Mass = new GUIContent("Mass", "Rigidbody 的质量");
                public static GUIContent Drag = new GUIContent("Drag", "对象的 Drag (阻力)");
                public static GUIContent AngularDrag = new GUIContent("AngularDrag", "对象的 Angular drag (角阻力)");
                public static GUIContent UseGravity = new GUIContent("UseGravity", "Rigidbody 是否受重力影响");
                public static GUIContent IsKinematic = new GUIContent("IsKinematic", "是否受物理运算影响");
                public static GUIContent Interpolate = new GUIContent("Interpolate", "通过使用interpolation，可以使固定帧率下物理处理执行的效果更加平滑。");
                public static GUIContent CollisionDetectionMode = new GUIContent("CollisionDetection", "Rigidbody 的碰撞检测模式");
                public static GUIContent Constraints = new GUIContent("Constraints", "控制Rigidbody模拟中可以自由操作的轴");
                public static GUIContent FreezePosition = new GUIContent("Freeze Position");
                public static GUIContent FreezeRotation = new GUIContent("Freeze Rotation");
            }

            RigidbodyKun m_rigidbodyKun;
            public RigidbodyKun rigidbodyKun
            {
                get
                {
                    if (m_rigidbodyKun == null)
                    {
                        m_rigidbodyKun = new RigidbodyKun();
                    }
                    return m_rigidbodyKun;
                }

                set
                {
                    m_rigidbodyKun = value;
                }
            }

            [SerializeField] bool m_foldoutConstraints;
            [SerializeField] bool m_rigidbodyFoldout = true;

            public override void SetComponentKun(ComponentKun componentKun)
            {
                rigidbodyKun = componentKun as RigidbodyKun;
            }


            public override ComponentKun GetComponentKun()
            {
                return rigidbodyKun;
            }

            public override bool OnGUI()
            {
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                m_rigidbodyFoldout = EditorGUITools.Foldout(m_rigidbodyFoldout, Styles.Rigidbody);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                if (m_rigidbodyFoldout)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        rigidbodyKun.mass = EditorGUILayout.FloatField(Styles.Mass, rigidbodyKun.mass);
#if UNITY_6000_0_OR_NEWER
                        rigidbodyKun.linearDamping = EditorGUILayout.FloatField(Styles.Drag, rigidbodyKun.linearDamping);
                        rigidbodyKun.angularDamping = EditorGUILayout.FloatField(Styles.AngularDrag, rigidbodyKun.angularDamping);
#else
                        rigidbodyKun.drag = EditorGUILayout.FloatField(Styles.Drag, rigidbodyKun.drag);
                        rigidbodyKun.angularDrag = EditorGUILayout.FloatField(Styles.AngularDrag, rigidbodyKun.angularDrag);
#endif
                        rigidbodyKun.useGravity = EditorGUILayout.Toggle(Styles.UseGravity, rigidbodyKun.useGravity);
                        rigidbodyKun.isKinematic = EditorGUILayout.Toggle(Styles.IsKinematic, rigidbodyKun.isKinematic);
                        rigidbodyKun.interpolation = (RigidbodyInterpolation)EditorGUILayout.EnumPopup(Styles.Interpolate, rigidbodyKun.interpolation);
                        rigidbodyKun.collisionDetectionMode = (CollisionDetectionMode)EditorGUILayout.EnumPopup(Styles.CollisionDetectionMode, rigidbodyKun.collisionDetectionMode);
                        m_foldoutConstraints = EditorGUITools.Foldout(m_foldoutConstraints, Styles.Constraints);
                        if (m_foldoutConstraints)
                        {
                            using (new EditorGUI.IndentLevelScope())
                            {
                                bool px = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX) ? true : false;
                                bool py = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezePositionY) == RigidbodyConstraints.FreezePositionY) ? true : false;
                                bool pz = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ) ? true : false;
                                bool rx = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezeRotationX) == RigidbodyConstraints.FreezeRotationX) ? true : false;
                                bool ry = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezeRotationY) == RigidbodyConstraints.FreezeRotationY) ? true : false;
                                bool rz = ((rigidbodyKun.constraints & RigidbodyConstraints.FreezeRotationZ) == RigidbodyConstraints.FreezeRotationZ) ? true : false;

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(Styles.FreezePosition);
                                px = EditorGUILayout.ToggleLeft("X", px);
                                py = EditorGUILayout.ToggleLeft("Y", py);
                                pz = EditorGUILayout.ToggleLeft("Z", pz);
                                GUILayout.FlexibleSpace();
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(Styles.FreezeRotation);
                                rx = EditorGUILayout.ToggleLeft("X", rx);
                                ry = EditorGUILayout.ToggleLeft("Y", ry);
                                rz = EditorGUILayout.ToggleLeft("Z", rz);
                                GUILayout.FlexibleSpace();
                                EditorGUILayout.EndHorizontal();

                                rigidbodyKun.constraints = RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= px ? RigidbodyConstraints.FreezePositionX : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= py ? RigidbodyConstraints.FreezePositionY : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= pz ? RigidbodyConstraints.FreezePositionZ : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= rx ? RigidbodyConstraints.FreezeRotationX : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= ry ? RigidbodyConstraints.FreezeRotationY : RigidbodyConstraints.None;
                                rigidbodyKun.constraints |= rz ? RigidbodyConstraints.FreezeRotationZ : RigidbodyConstraints.None;
                            }
                        }
                    }
                }
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

                return true;
            }

        }
    }
}