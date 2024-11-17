using UnityEditor;
using UnityEngine;

namespace SuperFrank
{
    [CustomPropertyDrawer(typeof(QuestItemKey))]
    public class QuestItemKeyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIContent[] subLabels = new GUIContent[] { new GUIContent("Item"), new GUIContent("Amount") };
            EditorGUI.MultiPropertyField(position, subLabels, property.FindPropertyRelative("item"), EditorGUI.PropertyVisibility.OnlyVisible);
        }
    }
}
