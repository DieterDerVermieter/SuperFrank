using UnityEngine;

namespace SuperFrank
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementFactor = 5.0f;
        [SerializeField] private float _aimSensitivity = 1.0f;

        [SerializeField] private float _baseHeight = 10.0f;

        private Quaternion _statePosition = Quaternion.identity;
        private float _height;

        private void Update()
        {
            Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            inputVector = inputVector.normalized;

            _statePosition *= Quaternion.Euler(
                inputVector.y * _movementFactor * Time.deltaTime,
                0.0f,
                -inputVector.x * _movementFactor * Time.deltaTime
                );

            _height = _baseHeight;

            transform.position = _statePosition * Vector3.up * _height;
            transform.rotation = _statePosition;
        }
    }
}
