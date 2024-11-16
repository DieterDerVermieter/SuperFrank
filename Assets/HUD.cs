using System;
using UnityEngine;
using TMPro; // Ensure TMPro namespace is included

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory; // The PlayerInventory script should be attached
    [SerializeField] private TMP_Text _coinText; // The TextMeshPro UI element to display the coin count

    private void Awake()
    {
        _playerInventory.EventCoinValueChanged += OnCoinValueChanged;
    }

    private void OnCoinValueChanged(int value)
    {
        _coinText.text = "Coins: " + value;
    }
}