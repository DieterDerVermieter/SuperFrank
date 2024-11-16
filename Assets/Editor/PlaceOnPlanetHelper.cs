using UnityEditor;
using UnityEngine;

namespace SuperFrank
{
    public class PlaceOnPlanetHelper : EditorWindow
    {
        private float _height;
        private Vector3 _euler;

        [MenuItem("Tools/PlaceOnPlanetHelper")]
        public static void Open()
        {
            GetWindow<PlaceOnPlanetHelper>().Show();
        }

        private void OnGUI()
        {
            _height = EditorGUILayout.FloatField(new GUIContent("Height"), _height);
            _euler = EditorGUILayout.Vector3Field(new GUIContent("Euler"), _euler);

            if (GUILayout.Button("Place"))
            {
                Planet planet = FindObjectOfType<Planet>();
                foreach (Transform transform in Selection.GetFiltered<Transform>(SelectionMode.Editable))
                {
                    Debug.Log($"Place {transform.name} on {planet.name}");
                    Place(planet, transform, _height, _euler);
                }
            }
        }

        private static void Place(Planet planet, Transform transform, float height, Vector3 euler)
        {
            Vector3 up = transform.position.normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, up);
            transform.rotation = Quaternion.Euler(euler) * rot;
            height += planet.GetBaseHeight(rot);
            transform.position = rot * Vector3.up * height;
        }
    }
}
