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

        [Header("Collisions")]
        [SerializeField] private float _collisionRadius = 1.0f;
        [SerializeField] private LayerMask _collisionMask;


        private Vector3 _velocity;

        private Vector2 _movementInput;
        private int _jumpBuffer = 100;

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
            _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_movementInput != Vector2.zero) _movementInput = _movementInput.normalized;

            if (Input.GetButtonDown("Jump"))
                _jumpBuffer = 0;

            float planeSpeed = _velocity.magnitude;
            _animator.SetFloat(_speedAnimId, planeSpeed + 1.0f);
        }

        private void FixedUpdate()
        {
            Vector3 currentUp = _rigidbody.position.normalized;
            Vector3 currentForward = _cameraPivot.forward;

            Quaternion rot = Quaternion.LookRotation(currentForward, currentUp);

            Vector2 movementForce = _movementInput * _movementSpeed * _movementDamping;
            _velocity.x += movementForce.x * Time.deltaTime;
            _velocity.z += movementForce.y * Time.deltaTime;

            _velocity.y -= _gravityForce * Time.deltaTime;

            if (_jumpBuffer < 6 && _isGrounded)
                _velocity.y = _jumpVelocity;
            _jumpBuffer++;

            _velocity.x /= 1.0f + _movementDamping * Time.deltaTime;
            _velocity.z /= 1.0f + _movementDamping * Time.deltaTime;

            if (_isGrounded)
            {
                // clamp velocity
                if (_velocity.y < 0.0f) _velocity.y = 0.0f;
            }

            _rigidbody.velocity = rot * _velocity;
            _rigidbody.rotation = rot;

            _isGrounded = false;
        }


        private void OnCollisionStay(Collision collision)
        {
            int count = collision.GetContacts(_contacts);
            for (int i = 0; i < count; i++)
            {
                _isGrounded |= Vector3.Dot(transform.up, _contacts[i].normal) > 0.7f;
            }
        }
    }
}
