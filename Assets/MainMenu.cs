using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperFrank
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private GameObject _creditsPanel, _optionsPanel;
        
        private void Awake()
        {
            _creditsPanel.SetActive(false);
            _optionsPanel.SetActive(false);
            
            _startButton.onClick.AddListener(OnClickStart);
            _creditsButton.onClick.AddListener(OnClickCredits);
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

        private void OnClickCredits()
        {
            SoundManager.Instance.PlayButtonSound();
            _creditsPanel.SetActive(true);
        }

        private void OnClickStart()
        {
            SoundManager.Instance.PlayButtonSound();
            SceneManager.LoadScene(1);
        }
    }
}
