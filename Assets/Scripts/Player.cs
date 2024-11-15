using UnityEngine;

namespace SuperFrank
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementFactor = 5.0f;
        [SerializeField] private float _aimSensitivity = 1.0f;

        [SerializeField] private float _baseHeight = 10.0f;
        [SerializeField] private Transform _cameraPivot;

        [SerializeField] private float _camPitchStrength = 1.0f;
        [SerializeField] private float _camPitchSmoothTime = 1.0f;

        private Quaternion _statePosition = Quaternion.identity;
        private float _height;

        private float _camPitchCurrent;
        private float _camPitchVelocity;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            inputVector = inputVector.normalized;

            Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            float camPitchTarget = -mouseVector.y * _camPitchStrength;
            _camPitchCurrent = Mathf.SmoothDamp(_camPitchCurrent, camPitchTarget, ref _camPitchVelocity, _camPitchSmoothTime, 100.0f);
            _cameraPivot.localRotation = Quaternion.Euler(_camPitchCurrent, 0.0f, 0.0f);

            _statePosition *= Quaternion.Euler(
                inputVector.y * _movementFactor * Time.deltaTime,
                mouseVector.x * _aimSensitivity * Time.deltaTime,
                -inputVector.x * _movementFactor * Time.deltaTime
                );

            _height = _baseHeight;

            transform.position = _statePosition * Vector3.up * _height;
            transform.rotation = _statePosition;
        }
    }
}
