using UnityEngine;

namespace SuperFrank
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Planet _planet;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Animator _animator;

        [Header("Movement")]
        [SerializeField] private float _movementSpeed = 5.0f;
        [SerializeField] private float _movementDamping = 10.0f;

        [Header("Jumping")]
        [SerializeField] private float _gravityForce = 10.0f;
        [SerializeField] private float _jumpVelocity = 30.0f;

        [Header("Aiming")]
        [SerializeField] private float _aimSensitivity = 1.0f;
        [SerializeField] private float _camPitchStrength = 1.0f;
        [SerializeField] private float _camPitchSmoothTime = 1.0f;


        private Quaternion _positionState = Quaternion.identity;
        private float _heightState;

        private Vector3 _positionEulerVelocity;
        private float _heightVelocity;

        private float _camPitchCurrent;
        private float _camPitchVelocity;

        private static readonly int _speedAnimId = Animator.StringToHash("speed");


        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Start()
        {
            _heightState = _planet.GetBaseHeight(Quaternion.identity);
        }

        private void Update()
        {
            Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            inputVector = inputVector.normalized;

            Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            bool jumpPressed = Input.GetButtonDown("Jump");

            float camPitchTarget = -mouseVector.y * _camPitchStrength;
            _camPitchCurrent = Mathf.SmoothDamp(_camPitchCurrent, camPitchTarget, ref _camPitchVelocity, _camPitchSmoothTime, 100.0f);
            _cameraPivot.localRotation = Quaternion.Euler(_camPitchCurrent, 0.0f, 0.0f);

            _positionEulerVelocity.x += inputVector.y * _movementSpeed * _movementDamping * Time.deltaTime;
            _positionEulerVelocity.z += -inputVector.x * _movementSpeed * _movementDamping * Time.deltaTime;

            _positionEulerVelocity.y = mouseVector.x * _aimSensitivity;

            _heightVelocity -= _gravityForce * Time.deltaTime;
            if (jumpPressed)
            {
                _heightVelocity = _jumpVelocity;
            }

            _positionState *= Quaternion.Euler(
                _positionEulerVelocity.x * Time.deltaTime,
                _positionEulerVelocity.y * Time.deltaTime,
                _positionEulerVelocity.z * Time.deltaTime
                );

            _positionEulerVelocity.x /= 1.0f + _movementDamping * Time.deltaTime;
            _positionEulerVelocity.z /= 1.0f + _movementDamping * Time.deltaTime;

            _heightState += _heightVelocity * Time.deltaTime;

            float baseHeight = _planet.GetBaseHeight(_positionState);
            if (_heightState < baseHeight)
            {
                _heightState = baseHeight;
                _heightVelocity = 0.0f;
            }

            transform.position = _positionState * Vector3.up * _heightState;
            transform.rotation = _positionState;

            float planeSpeed = new Vector2(_positionEulerVelocity.x, _positionEulerVelocity.z).magnitude;
            _animator.SetFloat(_speedAnimId, planeSpeed + 1.0f);
        }
    }
}
