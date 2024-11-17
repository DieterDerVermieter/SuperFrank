using System.Collections.Generic;
using UnityEngine;

namespace SuperFrank
{
    public enum QuestStatus
    {
        Waiting,
        Active,
        Done,
    }

    [CreateAssetMenu(menuName = "Quests/Quest")]
    public class Quest : ScriptableObject
    {
        public List<QuestItem> StartItems = new();
        public List<QuestItem> CollectItems = new();
        public List<QuestItem> RewardItems = new();
    }
}