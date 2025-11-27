using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    public static class EditorGUITools
    {
        static GUIStyle richTextLabel;
        public static GUIStyle RichTextLabel
        {
            get
            {
                if (richTextLabel != null)
                {
                    return richTextLabel;
                }
                richTextLabel = new GUIStyle(GUI.skin.label);
                richTextLabel.richText = true;
                return richTextLabel;
            }
        }

        public static bool Foldout(bool foldout, string content, bool toggleOnLabelClick=true)
        {
            return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick, EditorStyles.foldout);
        }
        public static bool Foldout(bool foldout, GUIContent content, bool toggleOnLabelClick = true)
        {
            return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick, EditorStyles.foldout);
        }
    }
}
