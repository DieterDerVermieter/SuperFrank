using UnityEngine;

[System.Serializable]
public class Quest
{
    public string QuestName;
    public bool IsCompleted { get; set; } = false;
}