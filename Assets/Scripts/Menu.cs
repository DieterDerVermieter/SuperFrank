using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton, exitButton, optionButton;
    [SerializeField] private TextMeshProUGUI creditCount;

    private void Awake() {
        playButton.onClick.AddListener(OnPlayButton);
        exitButton.onClick.AddListener(OnExitButton);
        optionButton.onClick.AddListener(OnOption);
    }
    public void OnPlayButton() 
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitButton ()
    {
        Application.Quit();
    }

    public void OnOption ()
    {
        
    }
}