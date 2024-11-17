using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Button[] _responseButtons;
    [SerializeField] private Button _stopDialogueButton;
    
    private Response[] _currentResponses;

    private void Awake()
    {
        // Ensure there is only one instance of DialogueManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _dialogueUI.SetActive(false);
        DeactivateAllButtons();
        _stopDialogueButton.onClick.AddListener(StopDialogue);
    }

    private void StopDialogue()
    {
        _dialogueUI.SetActive(false);
    }

    public void ShowDialogue(string dialogue)
    {
        _dialogueUI.SetActive(true);
        _dialogueText.text = dialogue;
    }

    public void DeactivateAllButtons()
    {
        foreach (Button button in _responseButtons)
        {
            button.gameObject.SetActive(false);
        }
        _stopDialogueButton.gameObject.SetActive(false);
    }
    
    public void HideDialogue()
    {
        _dialogueUI.SetActive(false);
    }
    
    public void ShowResponseDialogue(Response[] responses)
    {
        _currentResponses = responses;
        DeactivateAllButtons();

        for (int i = 0; i < _responseButtons.Length; i++)
        {
            if (i < responses.Length)
            {
                _responseButtons[i].gameObject.SetActive(true);
                _responseButtons[i].GetComponentInChildren<TMP_Text>().text = responses[i].responseText;

                int responseIndex = i;
                _responseButtons[i].onClick.RemoveAllListeners();
                _responseButtons[i].onClick.AddListener(() => OnResponseSelected(_responseButtons[responseIndex], responseIndex));
            }
            else
            {
                _responseButtons[i].gameObject.SetActive(false);
            }
        }

        // _stopDialogueButton.gameObject.SetActive(AreAnyResponseButtonsVisible());
    }

    private void OnResponseSelected(Button clickedButton, int responseIndex)
    {
        Response selectedResponse = _currentResponses[responseIndex];
        clickedButton.gameObject.SetActive(false);
        
        ShowDialogue(selectedResponse.reply);
        DeactivateAllButtons();

        // _stopDialogueButton.gameObject.SetActive(_responseButtons.Any(i=> i.IsActive()));
    }
    
    public bool AreAnyResponseButtonsVisible()
    {
        foreach (Button button in _responseButtons)
        {
            if (button.gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}