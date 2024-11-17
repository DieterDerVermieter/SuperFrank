using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SuperFrank
{
    public class PauseMenu : MonoBehaviour
    {
        [FormerlySerializedAs("_creditsButton")] [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private GameObject _creditsPanel, _optionsPanel;
        
        private void Awake()
        {
            _creditsPanel.SetActive(false);
            _optionsPanel.SetActive(false);
            
            _backButton.onClick.AddListener(OnClickBack);
            _exitButton.onClick.AddListener(OnClickExit);
            _optionsButton.onClick.AddListener(OnClickOptions);
        }

        private void OnClickExit()
        {
            SoundManager.Instance.PlayButtonSound();
            Application.Quit();
        }

        private void OnClickOptions()
        {
            SoundManager.Instance.PlayButtonSound();
            _optionsPanel.gameObject.SetActive(true);
        }

        private void OnClickBack()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SoundManager.Instance.PlayButtonSound();
            gameObject.SetActive(false);
        }

        
    }
}
