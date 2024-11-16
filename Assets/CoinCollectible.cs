using UnityEngine;

public class CoinCollectible : Collectible
{
    [SerializeField] private int _coinValue;

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
}