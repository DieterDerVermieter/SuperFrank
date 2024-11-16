using UnityEngine;

namespace SuperFrank
{
    [ExecuteInEditMode]
    public class PlaceOnPlanet : MonoBehaviour
    {
        [SerializeField] private float _groundDistance = 0.0f;
        [SerializeField] private bool _place;

        private void OnValidate()
        {
            if (_place)
            {
                _place = false;
                Planet planet = GetComponentInParent<Planet>();
                if (planet == null) return;
                Vector3 up = transform.position.normalized;
                transform.up = up;
                float height = planet.GetBaseHeight(transform.rotation) + _groundDistance;
                transform.position = transform.rotation * Vector3.up * height;
            }
        }
    }
}
