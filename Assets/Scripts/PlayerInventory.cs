using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int TotalCoins { get; private set; }
    public Action<int> EventCoinValueChanged;

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        Debug.Log("Coins collected. Total coins: " + TotalCoins);
        EventCoinValueChanged?.Invoke(TotalCoins);
    }
}