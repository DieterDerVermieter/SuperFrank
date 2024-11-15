using UnityEngine;

namespace SuperFrank
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private float _baseHeight = 10.0f;

        public float GetBaseHeight(Quaternion rotation)
        {
            return _baseHeight;
        }
    }
}
