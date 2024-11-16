using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;

    // Method to handle collection logic
    public virtual void Collect()
    {
        // Play collect sound
        if (_collectSound != null)
        {
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);
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