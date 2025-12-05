using PowerUtilities.UTJ;
using UnityEditor;
using Utj.UnityChoseKun.Editor;
using Utj.UnityChoseKun.Engine;

[MonoBehaviourKun(typeof(TestMonoKun))]
public class TestMonoView : MonoBehaviourView
{
    public override bool OnGUI()
    {
        var testMonoKun = behaviourKun as TestMonoKun;
        base.OnGUI();
        testMonoKun.path = EditorGUILayout.TextField(testMonoKun.path);
        return true;
    }
}
