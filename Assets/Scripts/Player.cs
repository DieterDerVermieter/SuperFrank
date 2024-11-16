using UnityEngine;

namespace SuperFrank
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Planet _planet;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _cameraPivot;

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


        private Vector3 _velocity;

        private Vector2 _movementInput;
        private Vector2 _aimInput;
        private int _jumpBuffer = 0;

        private bool _isGrounded;


        private static readonly int _speedAnimId = Animator.StringToHash("speed");

        private static readonly ContactPoint[] _contacts = new ContactPoint[16];


        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _movementInput += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _aimInput += new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            if (Input.GetButtonDown("Jump"))
                _jumpBuffer = 10;

            float planeSpeed = _velocity.magnitude;
            _animator.SetFloat(_speedAnimId, planeSpeed + 1.0f);
        }

        private void FixedUpdate()
        {
            if (_movementInput != Vector2.zero)
                _movementInput = _movementInput.normalized;

            Quaternion rot = _rigidbody.rotation;
            Vector3 forward = rot * Vector3.forward;
            Vector3 right = rot * Vector3.right;
            Vector3 up = rot * Vector3.up;

            Vector3 targetUp = _rigidbody.position.normalized;

            Quaternion rotDelta = Quaternion.FromToRotation(up, targetUp);
            rotDelta *= Quaternion.AngleAxis(_aimInput.x * _aimSensitivity * Time.deltaTime, targetUp);

            forward = rotDelta * forward;
            right = rotDelta * right;
            up = rotDelta * up;

            Quaternion rotTotal = Quaternion.LookRotation(forward, up);
            Quaternion rotInverse = Quaternion.Inverse(rotTotal);

            Vector3 velocity = _rigidbody.velocity;
            Vector3 velocity0 = rotInverse * velocity;

            velocity0.x += _movementInput.x * _movementSpeed * _movementDamping * Time.deltaTime;
            velocity0.z += _movementInput.y * _movementSpeed * _movementDamping * Time.deltaTime;

            velocity0.x /= 1.0f + _movementDamping * Time.deltaTime;
            velocity0.z /= 1.0f + _movementDamping * Time.deltaTime;

            velocity0.y -= _gravityForce * Time.deltaTime;

            if (_isGrounded && velocity0.y < 0.0f)
            {
                velocity0.y = 0.0f;
            }

            if (_isGrounded && _jumpBuffer > 0)
            {
                velocity0.y = _jumpVelocity;
                _jumpBuffer = 0;
            }

            velocity = rotTotal * velocity0;

            _rigidbody.velocity = velocity;
            _rigidbody.rotation = rotTotal;

            _movementInput = Vector2.zero;
            _aimInput = Vector2.zero;
            _isGrounded = false;
            _jumpBuffer--;
        }


        private void OnCollisionStay(Collision collision)
        {
            int count = collision.GetContacts(_contacts);
            for (int i = 0; i < count; i++)
            {
                _isGrounded |= Vector3.Dot(_rigidbody.rotation * Vector3.up, _contacts[i].normal) > 0.7f;
            }
        }
    }
}
