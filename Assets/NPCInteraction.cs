using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Response[] _responses;
    
    // Define the NPC dialogue
    [SerializeField] private string _initialDialogue;
    
    // Dialogue when the related quest is completed
    [SerializeField] private string _completedQuestDialogue;
    [SerializeField] private string _noResponseDialogue;

    // Name of the associated quest
    [SerializeField] private string _questName;

    // Check if player is close to the NPC
    private bool _isPlayerInRange;

    void Update()
    {
        // Check if the player presses the interaction key (e.g., "E")
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    // Display the dialogue
    void TriggerDialogue()
    {
        if (!string.IsNullOrEmpty(_questName) && QuestManager.Instance.IsQuestCompleted(_questName))
        {
            DialogueManager.Instance.ShowDialogue(_completedQuestDialogue);
        }
        else
        {
            if (DialogueManager.Instance.AreAnyResponseButtonsVisible())
            {
                DialogueManager.Instance.ShowDialogue(_initialDialogue);
            }
            else
            {
                DialogueManager.Instance.ShowDialogue(_noResponseDialogue);
            }
            
        }
        
        DialogueManager.Instance.ShowResponseDialogue(_responses);
    }

    // Detect when the player enters the NPC's interaction range
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            DialogueManager.Instance.ShowDialogue("Press 'E' to talk");
        }
    }

    // Detect when the player exits the NPC's interaction range
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            DialogueManager.Instance.HideDialogue();
            DialogueManager.Instance.DeactivateAllButtons();
        }
    }
}