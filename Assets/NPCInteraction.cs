using SuperFrank;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Quest _quest;

    [Header("Dialog")]
    [SerializeField] private Response[] _responses;
    
    // Define the NPC dialogue
    [SerializeField] private string _notReadyDialogue;
    [SerializeField] private string _initialDialogue;
    [SerializeField] private string _notCompletedDialogue;
    [SerializeField] private string _completeDialogue;
    [SerializeField] private string _doneDialogue;

    // // Dialogue when the related quest is completed
    // [SerializeField] private string _completedQuestDialogue;
    // [SerializeField] private string _noResponseDialogue;
    // 
    // // Name of the associated quest
    // [SerializeField] private string _questName;


    // Check if player is close to the NPC
    private bool _isPlayerInRange;


    public UnityEvent OnQuestStarted;
    public UnityEvent OnQuestCompleted;


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
        QuestStatus status = QuestManager.Instance.GetQuestStatus(_quest);
        bool hasStartItems = QuestManager.Instance.HasItems(_quest.StartItems);
        bool hasCollectItems = QuestManager.Instance.HasItems(_quest.CollectItems);
        switch (status)
        {
            case QuestStatus.Done:
            case QuestStatus.Waiting:
                if (hasStartItems)
                {
                    DialogueManager.Instance.ShowDialogue(_initialDialogue);
                    QuestManager.Instance.TakeItems(_quest.StartItems);
                    QuestManager.Instance.SetQuestStatus(_quest, QuestStatus.Active);
                    OnQuestStarted?.Invoke();
                }
                else
                {
                    DialogueManager.Instance.ShowDialogue(_notReadyDialogue);
                }
                break;
            case QuestStatus.Active:
                if (hasCollectItems)
                {
                    DialogueManager.Instance.ShowDialogue(_completeDialogue);
                    QuestManager.Instance.TakeItems(_quest.CollectItems);
                    QuestManager.Instance.GiveItems(_quest.RewardItems);
                    QuestManager.Instance.SetQuestStatus(_quest, QuestStatus.Done);
                    OnQuestCompleted?.Invoke();
                }
                else
                {
                    DialogueManager.Instance.ShowDialogue(_notCompletedDialogue);
                }
                break;
            // case QuestStatus.Done:
            //     DialogueManager.Instance.ShowDialogue(_doneDialogue);
            //     break;
        }

        // if (!string.IsNullOrEmpty(_questName) && QuestManager.Instance.IsQuestCompleted(_questName))
        // {
        //     DialogueManager.Instance.ShowDialogue(_completedQuestDialogue);
        // }
        // else
        // {
        //     if (DialogueManager.Instance.AreAnyResponseButtonsVisible())
        //     {
        //         DialogueManager.Instance.ShowDialogue(_initialDialogue);
        //     }
        //     else
        //     {
        //         DialogueManager.Instance.ShowDialogue(_noResponseDialogue);
        //     }
        //     
        // }
        // 
        // DialogueManager.Instance.ShowResponseDialogue(_responses);
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