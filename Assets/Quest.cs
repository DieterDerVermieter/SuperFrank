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
        public List<QuestItemKey> StartItems = new();
        public List<QuestItemKey> CollectItems = new();
        public List<QuestItemKey> RewardItems = new();
    }
}