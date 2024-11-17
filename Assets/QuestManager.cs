using SuperFrank;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestItemKey> _startItems;
    [SerializeField] private List<QuestItemKey> _currentItems;

    public static QuestManager Instance;

    private Dictionary<QuestItem, int> _items = new();
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

        GiveItems(_startItems);
    }


    private void Update()
    {
        _currentItems.Clear();
        foreach (var (item, amount) in _items)
        {
            _currentItems.Add(new QuestItemKey() { item = item, amount = amount });
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


    public int GetItemCount(QuestItem item)
    {
        return _items.TryGetValue(item, out int count) ? count : 0;
    }

    public void SetItemCount(QuestItem item, int count)
    {
        _items[item] = count;
    }


    public bool HasItems(IEnumerable<QuestItemKey> keys)
    {
        return keys.All(item => GetItemCount(item.item) >= item.amount);
    }

    public bool HasItems(IEnumerable<QuestItem> items)
    {
        return items.All(item => GetItemCount(item) > 0);
    }

    public void GiveItems(IEnumerable<QuestItemKey> keys)
    {
        foreach(QuestItemKey key in keys)
        {
            int count = GetItemCount(key.item);
            SetItemCount(key.item, count + key.amount);
        }
    }

    public void GiveItems(IEnumerable<QuestItem> items)
    {
        foreach (QuestItem item in items)
        {
            int count = GetItemCount(item);
            SetItemCount(item, count + 1);
        }
    }

    public void TakeItems(IEnumerable<QuestItemKey> keys)
    {
        foreach (QuestItemKey key in keys)
        {
            int count = GetItemCount(key.item);
            SetItemCount(key.item, count - key.amount);
        }
    }

    public void TakeItems(IEnumerable<QuestItem> items)
    {
        foreach (QuestItem item in items)
        {
            int count = GetItemCount(item);
            SetItemCount(item, count - 1);
        }
    }

    public void TakeItemsAll(IEnumerable<QuestItem> items)
    {
        foreach (QuestItem item in items)
        {
            SetItemCount(item, 0);
        }
    }
}