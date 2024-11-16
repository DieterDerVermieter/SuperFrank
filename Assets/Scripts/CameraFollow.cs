using UnityEngine;

namespace SuperFrank
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private Transform _forwardPivot;

        [Header("Aiming")]
        [SerializeField] private float _aimSensitivity = 1.0f;
        [SerializeField] private float _camPitchStrength = 1.0f;
        [SerializeField] private float _camPitchSmoothTime = 1.0f;


        private float _camPitchCurrent;
        private float _camPitchVelocity;

        private Quaternion _aimRotation = Quaternion.identity;


        private void LateUpdate()
        {
            Vector2 aimInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            float camPitchTarget = -aimInput.y * _camPitchStrength;
            _camPitchCurrent = Mathf.SmoothDamp(_camPitchCurrent, camPitchTarget, ref _camPitchVelocity, _camPitchSmoothTime, 100.0f);

            transform.position = _followTarget.position;
            transform.up = _followTarget.position.normalized;

            _forwardPivot.localRotation *= Quaternion.Euler(0.0f, aimInput.x * _aimSensitivity * Time.deltaTime, 0.0f);

            // transform.up = Quaternion.Euler(_camPitchCurrent, 0.0f, 0.0f) * _followTarget.position.normalized;
        }
    }
}
