using System;
using UnityEngine;
using TMPro; // Ensure TMPro namespace is included

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory; // The PlayerInventory script should be attached
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private GameObject _pauseMenu;// The TextMeshPro UI element to display the coin count

    private void Awake()
    {
        _playerInventory.EventCoinValueChanged += OnCoinValueChanged;
    }

    private void OnCoinValueChanged(int value)
    {
        _coinText.text = "Coins: " + value;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
            SetCursor(!_pauseMenu.activeInHierarchy);
        }
    }

    private void SetCursor(bool b)
    {
        if (b)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}