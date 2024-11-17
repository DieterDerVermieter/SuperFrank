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
            if (_camera!= null)
                transform.rotation = _camera.transform.rotation;
        }
    }
}
