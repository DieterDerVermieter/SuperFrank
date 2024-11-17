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
        [SerializeField] private float _groundedDistance = 1.1f;

        [Header("Aiming")]
        [SerializeField] private float _aimSensitivity = 1.0f;
        [SerializeField] private float _camPitchStrength = 1.0f;
        [SerializeField] private float _camPitchSmoothTime = 1.0f;

        [Header("Effects")]
        [SerializeField] private GameObject _landEffect;


        private Vector3 _velocity;

        private Vector2 _movementInput;
        private Vector2 _aimInput;
        private int _jumpBuffer = 0;

        private int _groundedCount;
        private bool _hasLanded = true;


        private static readonly int _speedAnimId = Animator.StringToHash("speed");
        private DialogueManager _dialogueManager;

        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _dialogueManager = DialogueManager.Instance;
        }

        private void Update()
        {
            if (_dialogueManager.AreAnyResponseButtonsVisible())
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
       
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

            bool isGrounded = false;
            bool isGroundedFull = false;
            if (Physics.Raycast(transform.position + _rigidbody.centerOfMass, -up, out RaycastHit hit, _groundedDistance))
            {
                isGrounded = true;
                isGroundedFull = hit.distance <= _groundedDistance * 0.9f;
            }

            Vector3 velocity = _rigidbody.velocity;
            Vector3 velocity0 = rotInverse * velocity;

            velocity0.x += _movementInput.x * _movementSpeed * _movementDamping * Time.deltaTime;
            velocity0.z += _movementInput.y * _movementSpeed * _movementDamping * Time.deltaTime;

            velocity0.x /= 1.0f + _movementDamping * Time.deltaTime;
            velocity0.z /= 1.0f + _movementDamping * Time.deltaTime;

            velocity0.y -= _gravityForce * Time.deltaTime;

            if (isGroundedFull && velocity0.y < 0.0f)
            {
                velocity0.y = 0.0f;
            }

            if (isGrounded && _jumpBuffer > 0)
            {
                velocity0.y = _jumpVelocity;
                _jumpBuffer = 0;
            }

            velocity = rotTotal * velocity0;

            _rigidbody.velocity = velocity;
            _rigidbody.rotation = rotTotal;

            if (isGrounded && !_hasLanded)
            {
                _hasLanded = true;
                GameObject effect = Instantiate(_landEffect, transform.position, transform.rotation);
                effect.AddComponent<DestroySystemWhenFinished>();
            }
            else if (!isGrounded)
            {
                _hasLanded = false;
            }

            _movementInput = Vector2.zero;
            _aimInput = Vector2.zero;
            _jumpBuffer--;
        }


        private void OnDrawGizmosSelected()
        {
            Vector3 start = transform.position + _rigidbody.centerOfMass;
            Vector3 end = start - transform.up * _groundedDistance;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(start, end);
            Gizmos.DrawSphere(end, 0.1f);
        }
    }
}