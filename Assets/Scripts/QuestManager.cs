using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> quests;

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

    private void Start()
    {
        LoadQuestStatus();
    }

    public void CompleteQuest(string questName)
    {
        Quest quest = quests.Find(q => q.QuestName == questName);
        if (quest != null)
        {
            quest.IsCompleted = true;
            SaveQuestStatus();
        }
    }

    public bool IsQuestCompleted(string questName)
    {
        Quest quest = quests.Find(q => q.QuestName == questName);
        return quest != null && quest.IsCompleted;
    }
    
    public void LoadQuestStatus()
    {
        foreach (var quest in quests)
        {
            quest.IsCompleted = PlayerPrefs.GetInt(quest.QuestName, 0) == 1;
        }
    }
    
    public void SaveQuestStatus()
    {
        foreach (var quest in quests)
        {
            PlayerPrefs.SetInt(quest.QuestName, quest.IsCompleted ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
    
    public void ResetAllSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}