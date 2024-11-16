using SuperFrank;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private GameObject _collectEffect;
    [SerializeField] private float _collectEffectScale = 1.0f;

    // Method to handle collection logic
    public virtual void Collect()
    {
        // Play collect sound
        if (_collectSound != null)
        {
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);
        }

        if (_collectEffect != null)
        {
            GameObject effect = Instantiate(_collectEffect, transform.position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * _collectEffectScale;
            effect.AddComponent<DestroySystemWhenFinished>();
        }

        // Add any additional collect logic here (e.g., adding to inventory)

        // Destroy the collectible object
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}