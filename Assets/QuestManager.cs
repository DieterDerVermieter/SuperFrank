using SuperFrank;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest _startQuest;

    [SerializeField] private List<Quest> _allQuests;

    [SerializeField] private UIQuestDisplay _displayPrefab;
    [SerializeField] private Transform _uiQuestContainer;
    [SerializeField] private GameObject _uiQuestHint;


    public static QuestManager Instance;

    private Dictionary<Quest, UIQuestDisplay> _displays = new();
    private bool _showQuests;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _startQuest.Data.IsActive = true;

        foreach (var quest in _allQuests)
        {
            _displays[quest] = Instantiate(_displayPrefab, _uiQuestContainer);
            _displays[quest].SetQuest(quest);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            _showQuests = !_showQuests;

        _uiQuestContainer.gameObject.SetActive(_showQuests);
        _uiQuestHint.gameObject.SetActive(!_showQuests);

        if (_showQuests)
        {
            foreach (var (quest, display) in _displays)
            {
                display.gameObject.SetActive(quest.Data.IsActive);
            }
        }
    }
}