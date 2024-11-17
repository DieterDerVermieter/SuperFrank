using SuperFrank;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<QuestItem> Items = new();
    private Dictionary<Quest, QuestStatus> _questStates = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public QuestStatus GetQuestStatus(Quest quest)
    {
        return _questStates.TryGetValue(quest, out QuestStatus status) ? status : QuestStatus.Waiting;
    }

    public void SetQuestStatus(Quest quest, QuestStatus status)
    {
        _questStates[quest] = status;
    }

    public bool HasItems(IEnumerable<QuestItem> items)
    {
        return items.All(item => Items.Contains(item));
    }

    public void GiveItems(IEnumerable<QuestItem> items)
    {
        Items.AddRange(items);
    }

    public void TakeItems(IEnumerable<QuestItem> items)
    {
        foreach (var item in items)
            Items.Remove(item);
    }
}