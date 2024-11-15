using UnityEngine;

namespace SuperFrank
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private bool _bakeHeights = false;

        [Header("Cache")]
        [SerializeField] private Collider[] _colliders;


        private void OnValidate()
        {
            if (_bakeHeights)
            {
                _bakeHeights = false;
                BakeHeights();
            }
        }

        private void BakeHeights()
        {
            _colliders = GetComponentsInChildren<Collider>();
        }


        public float GetBaseHeight(Quaternion rotation)
        {
            Vector3 dir = rotation * Vector3.up;
            Ray ray = new Ray(dir * 1000.0f, -dir);
            float smallestHeight = 1000.0f;

            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i].Raycast(ray, out RaycastHit hit, 1000.0f))
                {
                    float height = hit.point.magnitude;
                    if (height < smallestHeight)
                        smallestHeight = height;
                }
            }

            return smallestHeight;
        }
    }
}
