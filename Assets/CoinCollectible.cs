using UnityEngine;

public class CoinCollectible : Collectible
{
    [SerializeField] private int _coinValue;

    [SerializeField] private float _idleHeight = 1.0f;
    [SerializeField] private float _idleSpeed = 1.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _upAxis;
    private float _rotationY;
    private float _heightTimer;

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _upAxis = transform.position.normalized;

        _rotationY += Random.value * 90.0f;
        _heightTimer += Random.value;
    }

    public override void Collect()
    {
        // Add coins to the player's total
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.AddCoins(_coinValue);
        }
        
        // Call the base Collect method to handle common logic
        base.Collect();
    }

    private void Update()
    {
        _heightTimer += _idleSpeed * Time.deltaTime;
        float height = Mathf.Sin(_heightTimer);
        transform.position = _startPosition + Vector3.up * height * _idleHeight;
        _rotationY += _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(_rotationY, _upAxis) * _startRotation;
    }
}