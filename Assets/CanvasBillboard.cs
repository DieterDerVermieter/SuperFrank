using UnityEngine;

namespace SuperFrank
{
    public class CanvasBillboard : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {            
            transform.rotation = _camera.transform.rotation;
        }
    }
}
