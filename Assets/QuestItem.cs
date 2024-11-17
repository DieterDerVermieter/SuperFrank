using System;
using UnityEngine;

namespace SuperFrank
{
    [CreateAssetMenu(menuName = "Quests/Item")]
    public class QuestItem : ScriptableObject
    {
    }

    [Serializable]
    public struct QuestItemKey
    {
        public QuestItem item;
        public int amount;          
    }
}
