using UnityEditor;
using UnityEngine;

namespace SuperFrank
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            Quest quest = target as Quest;
            EditorGUILayout.Toggle(new GUIContent("IsActive"), quest.Data.IsActive);
            EditorGUILayout.IntField(new GUIContent("ItemCounter"), quest.Data.ItemCounter);
        }
    }
}
