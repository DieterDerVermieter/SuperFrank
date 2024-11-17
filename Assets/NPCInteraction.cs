using SuperFrank;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Quest[] _quests;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;

    [Header("Dialog")]

    // Define the NPC dialogue
    [SerializeField] private string _name = "Alex";
    [SerializeField] private string _noQuestDialogue = "Nothing to do";


    // Check if player is close to the NPC
    private bool _isPlayerInRange;


    private void Start()
    {
        _nameText.text = _name;
    }


    void Update()
    {
        // Check if the player presses the interaction key (e.g., "E")
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }

        bool hasActiveQuest = false;
        for (int i = 0; i < _quests.Length; i++)
        {
            if (_quests[i].Data.IsActive)
            {
                bool hasItems = _quests[i].Data.ItemCounter >= _quests[i].NeededAmount;
                _iconImage.color = hasItems ? Color.green : Color.yellow;
                hasActiveQuest = true;
                break;
            }
        }
        _iconImage.gameObject.SetActive(hasActiveQuest);
    }

    // Display the dialogue
    void TriggerDialogue()
    {
        bool hasActiveQuest = false;
        for (int i = 0; i < _quests.Length; i++)
        {
            if (_quests[i].Data.IsActive)
            {
                bool hasItems = _quests[i].Data.ItemCounter >= _quests[i].NeededAmount;
                if (!hasItems)
                {
                    DialogueManager.Instance.ShowDialogue(_quests[i].ActiveText);
                }
                else
                {
                    DialogueManager.Instance.ShowDialogue(_quests[i].DoneText);
                    _quests[i].Data.IsActive = false;
                    _quests[i].Data.ItemCounter = 0;
                    for (int j = 0; j < _quests[i].NextQuests.Length; j++)
                    {
                        _quests[i].NextQuests[j].Data.IsActive = true;
                    }
                }

                hasActiveQuest = true;
                break;
            }
        }

        if (!hasActiveQuest)
        {
            DialogueManager.Instance.ShowDialogue(_noQuestDialogue);
        }
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