using JayTools.Examples.JayLogs;
using JayTools.JayLogs;
using UnityEditor;
using UnityEngine;

namespace JayTools.Example.Editor
{
    [CustomEditor(typeof(DebugLogExample))]
    public class DebugLogExampleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Open Log File"))
            {
                JayLog.OpenLogFile(true);
            }
        }
    }
}